```zsh
# install dotnet-ef tool
Backend % dotnet tool install --global dotnet-ef
Backend % dotnet tool update --global dotnet-ef

# install packages
Backend % dotnet add package Microsoft.EntityFrameworkCore.Sqlite
Backend % dotnet add package Microsoft.EntityFrameworkCore.Design

# To unapply all migrations:
Backend % dotnet ef database update 0
# To remove all migrations
Backend % dotnet ef migrations remove
# To delete database
Backend % dotnet ef database drop

# Add a migration
Backend % dotnet ef migrations add Mig0 -o App_Data/Migrations
# Apply a migration
Backend % dotnet ef database update 
# Drop database
Backend %  dotnet ef database drop
# Remove migrations
Backend %  dotnet ef migrations remove
# Script all migrations
Backend % dotnet ef migrations script -o script.sql
# Script two migrations
Backend % dotnet ef migrations script Mig0 Mig1 -o script.sql

# [-p <ProjectHavingDbContext> -s <StartupProject> ]


```


```cs
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
```