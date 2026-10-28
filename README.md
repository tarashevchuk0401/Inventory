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
- [ ] `Product` / `Supplier` / `Customer` entities fleshed out (currently empty stubs) and their CRUD endpoints
- [ ] `Order` / `OrderItem` entities — order placement (decrement stock, compute total), the main feature
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
