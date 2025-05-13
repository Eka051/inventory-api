using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Debug environment variables
foreach (var env in Environment.GetEnvironmentVariables().Keys)
{
    Console.WriteLine($"Environment Variable: {env}");
}

// --- Load Connection String ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("DefaultConnection")
    ?? throw new InvalidOperationException("❌ Connection string 'DefaultConnection' not found.");
Console.WriteLine($"📡 Connection string found (length: {connectionString.Length})");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Logging.AddConsole();

// --- JWT ---
Console.WriteLine("Mencari JWT Key...");
var jwtKeyFromConfig = builder.Configuration["Jwt:Key"];
var jwtKeyFromEnv = Environment.GetEnvironmentVariable("Jwt__Key");
Console.WriteLine($"JWT Key from config: {(string.IsNullOrEmpty(jwtKeyFromConfig) ? "not found" : "found")}");
Console.WriteLine($"JWT Key from env: {(string.IsNullOrEmpty(jwtKeyFromEnv) ? "not found" : "found")}");

var jwtKey = jwtKeyFromConfig ?? jwtKeyFromEnv ?? builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    Console.WriteLine("JWT NOT FOUND");
}
else
{
    Console.WriteLine($"✅ JWT Key ditemukan (panjang: {jwtKey.Length})");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    });

// --- Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            Console.WriteLine("⚙️ Menjalankan database migration...");
            dbContext.Database.Migrate();
            Console.WriteLine("✅ Migrasi database selesai");
        }
        else
        {
            Console.WriteLine("✅ Database sudah up-to-date, tidak perlu migrasi");
        }

        if (!dbContext.Categories.Any())
        {
            Console.WriteLine("⚙ Database kosong, menjalankan seeder...");
            DbSeeder.Seed(dbContext);
            Console.WriteLine("✅ Seeding selesai");
        }
        else
        {
            Console.WriteLine("Database sudah berisi data, tidak perlu seeding");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error saat migrasi/seeding database: {ex.Message}");
}

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();