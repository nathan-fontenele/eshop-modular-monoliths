# eshop-modular-monoliths

## Objective
<p>This project is part of the course "Building Modular Monoliths with .NET 8" offered by Packt. The intention is to develop a project using the modular monolith architecture.</p>

## Folder Structure

### Bootstrapper
<p>Contain API project that expose the modules APIs.</p>

#### Purpose:
<p>Main entry point of the application</p>

#### Contents:
- API project acting as the gateway.
- Program configuration (services, middleware, routing).
- Dependency injection setup and module registration.
- Web host configuration and startup logic.

### Modules
<p>It contains various modules of our application, including the catalog basket, identity, and ordering.</p>

#### Purpose:
<p>Contains separate subfolders for each business domain modules.</p>

#### Design principles:
<p>Domain-Driven Design (DDD) + Vertical Slice Architecture.</p>

#### Examples:
- Catalog: Features, Data (EF Core configs, migrations, DbContext), Models (domain entities, value objects), Events (domain/integration events).

- Basket: Similar structure plus distributed caching configuration.

- Ordering: Handles order creation, updates, payments; implements outbox pattern for realiable messaging.

- Identity: Manages authentication and authorization.

### Shared
<p>Contain common and cross cutting concerns like logging validation exceptions, and so on.</p>

#### Purpose:
<p>Contains cross-cutting and reusable components used across modules.</p>

#### Subfloders/Class Libraries:
- Shared.Common: Utility classes, helpers, extentions, logging, exception handling, and other common functionalities.

- Shared.Contracts: Interfaces and abstract classes for synchronous inter-module communication.

- Shared.Messaging: Configurations and implementations for asynchronous messaging, plus integration events and event handlers.