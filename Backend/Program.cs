using log4net;
using log4net.Config;

XmlConfigurator.Configure(new FileInfo("log4net.config"));

ILog log4netLogger = LogManager.GetLogger(typeof(Program));

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton(log4netLogger);

    builder.Services.AddControllers();

    var app = builder.Build();

    app.UseHsts();

    app.UseHttpsRedirection();

    app.MapControllers();

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