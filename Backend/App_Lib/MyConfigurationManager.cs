namespace Backend;

public class MyConfigurationManager
{
    public static IConfiguration Data
    {
        get;
    }
    static MyConfigurationManager()
    {
        Data = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("data.json").Build();
    }
}
