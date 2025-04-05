# Comparativo LINQ vs Stored Procedure (.NET + SQL Server)

Esse projeto compara o desempenho entre uma query usando LINQ e outra via stored procedure, com benchmark simples via REST API.

## Stack
- ASP.NET Core 8
- EF Core
- SQL Server (Docker)

## Endpoints
- `/comparativo/linq`
- `/comparativo/procedure`

## Subir com Docker
```bash
docker compose up -d
```