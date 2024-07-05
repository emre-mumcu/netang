namespace Backend.App_Lib.Configuration;

// https://csharpindepth.com/articles/singleton (Sixth version)
public sealed class AppConfig
{
    private static readonly Lazy<AppConfig> instance = new Lazy<AppConfig>(() => new AppConfig());

    public static AppConfig Instance { get { return instance.Value; } }

    private AppConfig()
    {
        DataConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("data.json", true)
            .Build();
    }

    public IConfiguration DataConfiguration { get; }

    public IWebHostEnvironment? WebHostEnvironment { get; set; }
}