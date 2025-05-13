using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Load Connection String ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("DefaultConnection")
    ?? throw new InvalidOperationException("❌ Connection string 'DefaultConnection' not found.");
Console.WriteLine($"📡 Connection string: {connectionString}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Logging.AddConsole();

// --- JWT ---
var jwtKeyFromConfig = builder.Configuration["Jwt:Key"];
var jwtKeyFromEnv = Environment.GetEnvironmentVariable("Jwt__Key");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
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

if (args.Contains("seed"))
{
    Console.WriteLine("⚙️ Running database migration + seeder...");
    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        DbSeeder.Seed(db);
    }

    Console.WriteLine("✅ Seeding selesai.");
    return;
}

var appNormal = builder.Build();

if (appNormal.Environment.IsDevelopment() || true)
{
    appNormal.UseSwagger();
    appNormal.UseSwaggerUI();
}

appNormal.UseHttpsRedirection();
appNormal.UseRouting();
appNormal.UseAuthentication();
appNormal.UseMiddleware<ErrorHandlingMiddleware>();
appNormal.UseAuthorization();
appNormal.MapControllers();
appNormal.Run();