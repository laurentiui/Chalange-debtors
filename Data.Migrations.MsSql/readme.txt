cd [SOLUTION_FOLDER]
dotnet ef migrations add "initial" -s WebApi/WebApi.csproj -p Data.Migrations.MsSql
dotnet ef database update -s WebApi/WebApi.csproj -p Data.Migrations.MsSql