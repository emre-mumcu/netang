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

        // In order to seed the data by this way, migrations (add migration and update database) must be run
        // modelBuilder.SeedData();
    }

    public virtual DbSet<TraceEntity> Traces => Set<TraceEntity>();
}


/*
Steps for using encrypted SQLite database in your .Net application with EFCore
This example is using .Net Core 3.1 with EFCore 3.1.6 and SQLCipher 2.0.3:

Add a NuGET reference to Microsoft.EntityFrameworkCore in your project
Add a NuGET reference to Microsoft.EntityFrameworkCore.Design in your project
Add a NuGET reference to Microsoft.EntityFrameworkCore.Sqlite.Core in your project This is a really important step: DO NOT add a reference to Microsoft.EntityFrameworkCore.Sqlite, otherwise it will not work!
Add a NuGET reference to SQLitePCLRaw.bundle_e_sqlcipher
Create a connection object (SqliteConnection), defining the database access password in the connection string (you can use SqliteConnectionStringBuilder) you give to the constructor
Give the connection object to the UseSqlite method, when configuring the database context

*/