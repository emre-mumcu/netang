using System.Security.Cryptography;
using System.Text;
using Backend.App_Lib.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Backend.App_Lib.Configuration.Services;

public static class _Authentication
{
    public static IServiceCollection _AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AppConfig.Instance.DataConfiguration["TokenParameters:Issuer"],
                ValidAudience = AppConfig.Instance.DataConfiguration["TokenParameters:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(AppConfig.Instance.DataConfiguration["TokenParameters:SignKey"]!)))
            };
        });

        return services;
    }

    public static IApplicationBuilder _UseAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        
        return app;
    }
}
