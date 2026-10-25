# InventoryHub

## - Swagger :http://localhost:5184/swagger/index.html

An inventory & order management Web API — learning project built incrementally while working through a .NET learning plan (full curriculum lives in a separate repo: `~/Projects/learning/dotNet/learning-plan.md`).

## Architecture

Clean/Onion-style layering, four projects, dependencies flow inward:

```
InventoryHub.Api             — ASP.NET Core Web API (controllers), DI wiring, Swagger
   → InventoryHub.Application
   → InventoryHub.Infrastructure
InventoryHub.Infrastructure   — repository implementations (in-memory now, EF Core + PostgreSQL later)
   → InventoryHub.Domain
   → InventoryHub.Application
InventoryHub.Application      — use cases / services, repository interfaces
   → InventoryHub.Domain
InventoryHub.Domain           — plain entities, no dependencies
```

## Build plan

- [x] Solution scaffold — 4 projects, references wired
- [x] Swagger UI
- [x ] `User` entity + `GET /api/users` — first vertical slice (in-memory repo), to see the full `Controller → Service → IRepository → Repository` flow and where the DI container substitutes the concrete implementation
- [ ] JWT authentication — hashed passwords, login endpoint issuing a JWT, `[Authorize]` on protected endpoints
- [ ] Product / Order / Supplier entities and their endpoints
- [ ] Swap in-memory repositories for EF Core + PostgreSQL, first migration
- [ ] Entity relationships (Orders↔OrderItems↔Products, many-to-many Products↔Suppliers), LINQ-to-Entities reporting queries
- [ ] xUnit tests (`InventoryHub.Tests`), Repository/Service pattern formalized
- [ ] Dockerfile + docker-compose with PostgreSQL
- [ ] Angular frontend consuming this API
- [ ] Full docker-compose (API + PostgreSQL + frontend), polish

## Running locally

```bash
dotnet run --project InventoryHub.Api
```

`.http` request examples: `InventoryHub.Api/InventoryHub.Api.http`
