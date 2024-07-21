using Backend.App_Data.Entities;
using Backend.App_Lib.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Backend.App_Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(connectionString: AppConfig.Instance.DataConfiguration["Database:ConnectionString"]);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            // Static Seeder
            modelBuilder.SeedData();
        }

        // ModelBuilder Seeder
        // In order to seed the data by this way, migrations (add migration and update database) must be run
        // modelBuilder.SeedData();
    }

    public virtual DbSet<Trace> Traces => Set<Trace>();
    public virtual DbSet<State> States => Set<State>();
    public virtual DbSet<City> Cities => Set<City>();
    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Photo> Photos => Set<Photo>();
}