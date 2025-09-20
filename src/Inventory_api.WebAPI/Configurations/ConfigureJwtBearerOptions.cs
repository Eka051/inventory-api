using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace Inventory_api.WebAPI.Configurations
{
    public class ConfigureJwtBearerOptions : IConfigureOptions<JwtBearerOptions>
    {
        public void Configure(JwtBearerOptions options)
        {
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new {success = false, message = "Unauthorized. Please login & authenticated"});
                    return context.Response.WriteAsync(result);
                }
            };
        }
    }
}
