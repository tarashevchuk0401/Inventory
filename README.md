# InventoryHub

## - Swagger :http://localhost:5184/swagger/index.html

An inventory & order management Web API — learning project built incrementally while working through a .NET learning plan (full curriculum lives in a separate repo: `~/Projects/learning/dotNet/learning-plan.md`).

## What it does

A small business tracks products it sells, the suppliers it buys them from, and the customers who order them:

- **Product catalog** — products with a price and a stock quantity, each supplied by a `Supplier`.
- **Customers** — the people placing orders.
- **Order processing** (the main feature) — a `Customer` places an `Order` made up of one or more `OrderItem`s (a product + quantity + unit price at the time of the order). Placing an order decrements product stock and computes the order total from its items. Orders carry a status (e.g. `Pending` → `Fulfilled` → `Cancelled`) so fulfillment can be tracked over time.
- **Reporting** — once orders exist, the interesting LINQ-heavy queries become possible: top-selling products, revenue by customer, low-stock alerts (products whose stock has dropped below a threshold).
- **Auth** — users sign up/sign in and get a JWT; protected endpoints require a bearer token (already built — see below).

## Entities

| Entity | Purpose | Key fields (planned) |
|---|---|---|
| `Product` | Something sold | `Id`, `Name`, `Sku`, `Price`, `StockQuantity`, `SupplierId` |
| `Supplier` | Who a product is sourced from | `Id`, `Name`, `ContactEmail`, `Phone` |
| `Customer` | Who places orders | `Id`, `FirstName`, `LastName`, `Email`, `Phone` |
| `Order` | A purchase by a customer | `Id`, `CustomerId`, `OrderDate`, `Status`, `OrderItems` (collection) |
| `OrderItem` | A line on an order | `Id`, `OrderId`, `ProductId`, `Quantity`, `UnitPriceAtOrder` |
| `User` | Auth identity (not a storefront concept) | `Id`, `FirstName`, `LastName`, `Email`, `HashedPassword`, `Role` |

`Product`, `Supplier`, and `Customer` currently exist as empty stub classes in `InventoryHub.Domain/Entities` — fields above are the plan, not yet implemented. `User` is fully built (see Build plan).

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
- [x] `User` entity + `GET /api/users` — first vertical slice (in-memory repo), to see the full `Controller → Service → IRepository → Repository` flow and where the DI container substitutes the concrete implementation
- [x] JWT authentication — hashed passwords, login endpoint issuing a JWT, authenticated-by-default fallback policy with `[AllowAnonymous]` on sign-up/sign-in
- [x] `Product` / `Supplier` / `Customer` entities fleshed out (currently empty stubs) and their CRUD endpoints
- [ ] `Order` / `OrderItem` entities — order placement (decrement stock, compute total), the main feature
- [x] Swap in-memory repositories for EF Core + PostgreSQL, first migration
- [ ] Entity relationships (Orders↔OrderItems↔Products, many-to-many Products↔Suppliers), LINQ-to-Entities reporting queries
  - [ ] Low-stock alert — `IProductRepository.GetLowStockAsync(threshold)`, `Where` + `OrderBy` ascending on `StockQuantity`
  - [ ] Order summary projection — per-`Order` `{ ItemCount, Total }` via nested aggregates (`Count()`/`Sum()`) over `Order.OrderItems`
  - [ ] Products never ordered — anti-join (`!Any()`) of `Products` against `OrderItems`
  - [ ] Top-selling products — `GroupBy(ProductId)` + `Sum(Quantity)` + `OrderByDescending` + `Take(n)`, joined back to `Product` for the name
  - [ ] Revenue by customer — `OrderItem → Order → CustomerId`, sum `Quantity * UnitPriceInOrder` grouped by customer
  - [ ] Paging — `Skip`/`Take` on `Product.GetAllAsync`, with explicit `OrderBy` (demonstrate the undefined-order bug first, then fix it)
- [ ] xUnit tests (`InventoryHub.Tests`), Repository/Service pattern formalized
- [x] Dockerfile + docker-compose with PostgreSQL
- [ ] Angular frontend consuming this API
- [ ] Full docker-compose (API + PostgreSQL + frontend), polish

## Running locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL running locally on port `5432` (only needed if you want the database-backed `Product` repository — see [Switching between in-memory and database](#switching-between-in-memory-and-database) below; everything else still works without it)

### 1. Configure secrets

Two values aren't committed to the repo (connection string, JWT signing key) — they're kept in [.NET user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets), a local-only file outside the repo. Set them once, from the repo root:

```bash
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres" --project InventoryHub.Api
dotnet user-secrets set "Jwt:Key" "use-a-long-random-secret-at-least-32-characters!!" --project InventoryHub.Api
```

(Use your own values if your local Postgres credentials differ, or generate your own JWT key — it just needs to be at least 32 characters.)

### 2. Apply database migrations (optional — only if using the database repository)

After updating enity run from root
````bash
dotnet ef migrations add AddSupplierAddress --project InventoryHub.Infrastructure --startup-project InventoryHub.Api
````
And apply it
```bash
dotnet ef database update --project InventoryHub.Infrastructure --startup-project InventoryHub.Api
```

### 3. Run

```bash
dotnet run --project InventoryHub.Api
```

Swagger UI: http://localhost:5184/swagger/index.html
`.http` request examples: `InventoryHub.Api/InventoryHub.Api.http`

### Switching between in-memory and database

`IProductRepository` can be backed by either an in-memory `List<T>` (seeded with sample data, no setup required) or EF Core + PostgreSQL, controlled by the `Persistence:UseDatabase` setting:

| | `UseDatabase` | Where it's set |
|---|---|---|
| In-memory (default outside Development) | `false` | `InventoryHub.Api/appsettings.json` |
| Database (default for local `dotnet run`) | `true` | `InventoryHub.Api/appsettings.Development.json` |

To override it for a single run without editing any file:

```bash
Persistence__UseDatabase=false dotnet run --project InventoryHub.Api   # in-memory
Persistence__UseDatabase=true  dotnet run --project InventoryHub.Api   # database
```

In Rider/Visual Studio, pick the **`http (In-Memory)`** or **`http (Database)`** launch profile from the run configuration dropdown (defined in `InventoryHub.Api/Properties/launchSettings.json`) instead of using an env var.

The other five repositories (`Supplier`, `Customer`, `Order`, `OrderItem`, `User`) are still in-memory only — this flag currently only applies to `Product`.
