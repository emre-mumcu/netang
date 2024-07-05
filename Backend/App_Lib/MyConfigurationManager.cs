namespace Backend;

/// <summary>
/// Application Configuration Manager
/// </summary>
public class CM
{
    public static IConfiguration DataConfiguration { get; }

    static CM()
    {
        
        DataConfiguration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("data.json").Build();
    }
}
