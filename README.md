# Administrative Document API

## ## Migration

Add a migration in the project

```sh
Add-Migration <MigrationName> -Project Infrastructure.Persistence -Context ApplicationDbContext
// or
dotnet ef migrations add **MigrationName** --project Infrastructure.Persistence
```

## Build

Build project

```sh
dotnet build ServerMonitoring/ServerMonitoring.csproj
```

## Publishing

```sh
dotnet publish ServerMonitoring/ServerMonitoring.csproj -o /var/www/server-monitoring
```

## Linux services

```sh
systemctl enable dotnet-server-monitoring.service
systemctl start dotnet-server-monitoring.service
```