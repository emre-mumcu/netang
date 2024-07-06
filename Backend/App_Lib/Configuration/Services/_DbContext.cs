using Microsoft.EntityFrameworkCore;

namespace Backend.App_Lib.Configuration.Services;

public static class _DbContext
{
    public static IServiceCollection _AddDbContext<T>(this IServiceCollection services) where T: DbContext
    {
        // DbContext:

        // In this type of service registration, connection string is NOT provided to IoC.
        // It must be provided in DbContext's OnConfiguring method.
        services.AddDbContext<T>();

        // In this type of service registration, connection string is provided to IoC.
        // If DbContext is created by DI, connection string is present in the instance.
        // But if user manually creates DbContext, the connection string must also be provided in DbContext's OnConfiguring method
        // services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString: ""));

        // Manual Configuration        
        // builder.Services.AddScoped(x => { return new AppDbContext(); });           

        return services;
    }
}