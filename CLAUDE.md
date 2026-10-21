# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

InventoryHub is a learning project: an inventory & order management Web API (products, categories, suppliers, customers, orders, order items, stock levels), built incrementally while working through a personal .NET learning plan (an Angular/Node developer learning backend .NET). It is currently a fresh scaffold — all four projects exist with correct references wired up, but contain only template placeholder code (`WeatherForecastController`, `Class1.cs` stubs). No domain entities, business logic, or persistence have been written yet.

## Commands

```bash
# Build the whole solution
dotnet build

# Run the API project
dotnet run --project InventoryHub.Api

# Check a project's references
dotnet list <ProjectFolder> reference      # e.g. dotnet list InventoryHub.Api reference

# Add/remove a project reference
dotnet add <ProjectFolder> reference <OtherProjectFolder>
dotnet remove <ProjectFolder> reference <OtherProjectFolder>
```

There is no test project yet (planned for later — see roadmap below) and no linter/formatter config in the repo.

## Architecture

Clean/Onion-style layering across four projects in `InventoryHub.slnx`, with dependencies flowing inward toward `Domain`:

```
InventoryHub.Api             (Microsoft.NET.Sdk.Web — controllers, DI wiring, Swagger/OpenAPI)
   → InventoryHub.Application
   → InventoryHub.Infrastructure
InventoryHub.Infrastructure   (Microsoft.NET.Sdk — repository implementations)
   → InventoryHub.Domain
   → InventoryHub.Application
InventoryHub.Application      (Microsoft.NET.Sdk — use cases, repository interfaces, LINQ queries)
   → InventoryHub.Domain
InventoryHub.Domain           (Microsoft.NET.Sdk — plain entities, no dependencies on anything else)
```

- **`InventoryHub.Domain`**: plain C# entities only (e.g. `Product`, `Customer`, `Order`, `OrderItem`, `Supplier`). Must never reference ASP.NET Core, EF Core, or any other project in the solution.
- **`InventoryHub.Application`**: business logic/use cases and the repository *interfaces* (e.g. `IProductRepository`) that `Infrastructure` implements. This is where LINQ-heavy reporting queries belong (top-selling products, revenue by customer, low-stock alerts). Depends only on `Domain`.
- **`InventoryHub.Infrastructure`**: concrete repository implementations. Starts as in-memory `List<T>`-backed repositories; will be swapped for EF Core + PostgreSQL without changing `Application` or `Api`.
- **`InventoryHub.Api`**: ASP.NET Core Web API (controller-based, not minimal APIs — scaffolded with `dotnet new webapi -controllers`). Wires everything together via the DI container (`builder.Services`).

All four projects currently build but contain only scaffold-generated placeholder code.

## Roadmap (driven by the learning plan)

This project is built up week-by-week as part of a structured .NET learning plan (full plan lives in a separate repo: `~/Projects/learning/dotNet/learning-plan.md` and `~/Projects/learning/dotNet/topics/`). Expect the codebase to evolve roughly as follows — use this to judge what's "not built yet" vs "missing":

- **Now**: scaffold only (current state) — solution/project structure and references exist; no domain code yet.
- **Next**: `Domain` entities (`Product`, `Customer`, `Order`, `OrderItem`, `Supplier`); `Application` repository interfaces and first use cases; `Infrastructure` in-memory `List<T>` repository implementations; `Api` controllers for CRUD on products/orders wired through DI; Swagger enabled; a small custom middleware.
- **After that**: `appsettings.json`-driven configuration, logging, global error-handling middleware; `Infrastructure`'s in-memory repositories replaced with EF Core `DbContext` + PostgreSQL, first migration.
- **Then**: EF Core relationships between entities (Orders↔OrderItems↔Products, many-to-many Products↔Suppliers), async LINQ-to-Entities queries for the reporting features mentioned above.
- **Then**: `InventoryHub.Tests` (xUnit + Moq) added; Repository/Service pattern formalized; Dockerfile + docker-compose with PostgreSQL.
- **Then**: remaining backend edge cases/validation finished; an Angular frontend added (separate from this solution) consuming this API.
- **Finally**: full docker-compose (API + PostgreSQL + frontend), README, polish.

When asked to help with this project, infer which stage is current from what already exists in the code rather than assuming the roadmap's order is rigid — the user may jump around or revisit earlier stages for practice.
