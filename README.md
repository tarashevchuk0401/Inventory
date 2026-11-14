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

All entities above are fully implemented (not stubs). `Order.Status` and `Product.Category` are enums (`OrderStatus`: `Pending`/`Fulfilled`/`Cancelled`; `ProductCategories`: `Phone`/`Laptop`/`Console`/`Headphones`/`Tablet`/`PC`/`Other`).

## Architecture

Clean/Onion-style layering, four projects, dependencies flow inward:

```
InventoryHub.Api             — ASP.NET Core Web API (controllers), DI wiring, Swagger
   → InventoryHub.Application
   → InventoryHub.Infrastructure
InventoryHub.Infrastructure   — repository implementations (EF Core + PostgreSQL)
   → InventoryHub.Domain
   → InventoryHub.Application
InventoryHub.Application      — use cases / services, repository interfaces
   → InventoryHub.Domain
InventoryHub.Domain           — plain entities, no dependencies
```

Repositories are EF Core + PostgreSQL only — the earlier in-memory `List<T>` implementations were removed once the EF repositories covered every entity.

## Build plan

- [x] Solution scaffold — 4 projects, references wired
- [x] Swagger UI
- [x] `User` entity + `GET /api/users` — first vertical slice, to see the full `Controller → Service → IRepository → Repository` flow and where the DI container substitutes the concrete implementation
- [x] JWT authentication — hashed passwords, login endpoint issuing a JWT, authenticated-by-default fallback policy with `[AllowAnonymous]` on sign-up/sign-in
- [x] `Product` / `Supplier` / `Customer` entities fleshed out and their CRUD endpoints (create/read so far — update/delete are a practice task below)
- [x] `Order` / `OrderItem` entities — order placement (decrement stock, compute total)
- [x] Swap in-memory repositories for EF Core + PostgreSQL, first migration
- [x] Global exception-handling middleware (`AppExceptionHandler` + `AppException` hierarchy: `BadRequest`/`Unauthorized`/`AccessDenied`/`NotFound`/`Conflict`, mapped to ProblemDetails)
- [x] Entity relationships (Orders↔OrderItems↔Products), LINQ-to-Entities reporting queries
  - [x] Low-stock alert — `IProductRepository.GetLowStockAsync(threshold)`, `Where` + `OrderBy` ascending on `StockQuantity`
  - [x] Order statuses and product categories
  - [x] Order summary projection — per-`Order` `{ ItemCount, Total }` via nested aggregates (`Count()`/`Sum()`) over `Order.OrderItems`
    - [x] Products never ordered — anti-join (`!Any()`) of `Products` against `OrderItems`
    - [x] Top-selling products — `GroupBy(ProductId)` + `Sum(Quantity)` + `OrderByDescending` + `Take(n)`, joined back to `Product` for the name
    - [x] Revenue by customer — `OrderItem → Order → CustomerId`, sum `Quantity * UnitPriceInOrder` grouped by customer
    - [x] Paging — generic `PaginationRequestDto`/`PaginationRequestDto<TSortBy>`, applied to `Product`/`Order`/`Customer` listings
- [ ] Many-to-many Products↔Suppliers (currently one-to-many via `Product.SupplierId`) — see practice tasks below
- [ ] Add mapper (Mapperly)
- [ ] xUnit tests (`InventoryHub.Tests`), Repository/Service pattern formalized
- [x] Dockerfile + docker-compose with PostgreSQL
- [ ] Angular frontend consuming this API
- [ ] Full docker-compose (API + PostgreSQL + frontend), polish

## Practice tasks

Picked to drill three things: EF Core joins/relationships, custom exceptions, and writing endpoints end-to-end (`Controller → Service → Repository`).

1. **Delete a `Supplier`, guarded by a join** — add `DELETE /api/suppliers/{id}`. Before deleting, query whether any `Product` still references that `SupplierId`; if so, throw a new `ConflictException` ("cannot delete a supplier with existing products") instead of deleting. Exercises: a FK-aware query (join/`Any()`), a new failure path through the existing `AppException` pipeline, and a full endpoint from controller down to repository.

2. **Update endpoints for `Product` and `Customer`** — add `PUT /api/products/{id}` and `PUT /api/customers/{id}`. Throw `NotFoundException` if the id doesn't exist; for `Product`, also throw `BadRequestException` if the incoming price or stock quantity is negative. Exercises: validation exceptions beyond just 404s, and the update half of CRUD you haven't written yet.

3. **Finish `OrderItemController`'s commented-out `AddOrderItem`** — add an item to an *existing* order. Reuse the stock-decrement + insufficient-stock `ConflictException` logic already in `OrderService.AddAsync`, but also throw a new exception (e.g. `ConflictException`) if the target order's `Status` isn't `Pending` (no adding items to a `Fulfilled`/`Cancelled` order). Exercises: reusing/extracting shared logic, a status-based business rule, and completing a half-built endpoint.

4. **`PATCH /api/orders/{id}/status`** — transition an order's status (e.g. `Pending → Fulfilled` or `Pending → Cancelled`). Reject illegal transitions (e.g. `Cancelled → Fulfilled`) with `ConflictException`. On cancellation, restock every `OrderItem`'s `Quantity` back onto its `Product` — this means loading `Order.OrderItems` and joining back to `Product` to increment `StockQuantity` for each one. Exercises: a multi-table write inside one transaction (`IUnitOfWork.SaveChangesAsync`), joining across `Order → OrderItem → Product`, and a guarded state-machine-style exception.

5. **Real many-to-many: `Product` ↔ `Supplier`** — a product can be supplied by more than one supplier (at possibly different prices). Replace `Product.SupplierId` with a join entity (e.g. `ProductSupplier { ProductId, SupplierId, Price }`), configure it in EF (`HasMany().WithMany().UsingEntity<ProductSupplier>(...)` or an explicit join entity), and generate a migration. Then add `GET /api/products/{id}/suppliers` returning each supplier plus their price for that product — a genuine three-table join (`Product` ⋈ `ProductSupplier` ⋈ `Supplier`), not just a FK lookup. This is the biggest task — good for practicing EF Core many-to-many config specifically.

Suggested order: 1 → 2 → 3 → 4 → 5 (exceptions get progressively more interesting, joins go from a lookup to a real many-to-many).

## Running locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL running locally on port `5432` — all repositories are EF Core + PostgreSQL, there's no in-memory fallback

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
