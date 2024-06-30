using System.Security.Cryptography;
using System.Text;
using Backend;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
XmlConfigurator.Configure(new FileInfo("log4net.config"));

ILog log4netLogger = LogManager.GetLogger(typeof(Program));

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton(log4netLogger);

    builder.Services.AddControllers();

    builder.Services.AddScoped<ITokenService, TokenService>();

    builder.Services.AddAuthentication(opt =>
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
            ValidIssuer = MyConfigurationManager.Data["JWT:Issuer"],
            ValidAudience = MyConfigurationManager.Data["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(MyConfigurationManager.Data["JWT:SecurityKey"]!)))
        };
    });

    var app = builder.Build();

    app.UseHsts();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    app.Run();

    log4netLogger.Info("Program started");
}
catch (Exception ex)
{
    log4netLogger.Error($" Program error: {ex.Message}");
}



/*

dotnet add package log4net
[optional]
dotnet add package log4net.Ext.Json

git config --global user.name "Emre Mumcu"
git config --global user.email "emre@mumcu.net"
git config --global http.sslVerify false
git config --global init.defaultBranch <name>

git init
git init -b main
git add -A
git commit -m "First Commit"
git branch -M maian
git remote add origin https://github.com/emre-mumcu/netang.git
git remote -v
git push -u origin main

git add -A
git commit -m “Second Commit”
git push -u origin main
*/