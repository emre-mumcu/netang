dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Design

% dotnet ef migrations add Mig0  [-c ProjectHavingDbContext] [-s StartupProject] [-o Migrations/Folder]
% dotnet ef migrations add Mig0 -o App_Data\Migrations
% dotnet ef database update  [-c ProjectHavingDbContext] [-s StartupProject] 
% dotnet ef database drop
% dotnet ef migrations remove

% dotnet ef migrations script -o script.sql
% dotnet ef migrations script Mig0 Mig1 -o script.sql [-c ProjectHavingDbContext] [-s StartupProject] 
% dotnet ef migrations script --help