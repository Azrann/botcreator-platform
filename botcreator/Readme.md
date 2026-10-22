**Languages:** [English](Readme.md) · [Español](README.es.md)

# botcreator

Backend for creating, deploying, and managing customer support bots, web widgets, users, and metrics.

Documentation based on local files reviewed on 21 October 2026.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2020.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2020-03-24**, [`botcreator/src/Functions/Functions/Functions/Status.cs`](src/Functions/Functions/Functions/Status.cs); date of the commit modifying the file, recovered from local Git history.
- First record in the original Git repository containing this code: **2019-01-18**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [Microsoft.Extensions.DependencyInjection 3.1.2](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/3.1.2), published on **2020-02-18** (declared version), in [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](src/BotCreator.Api/BotCreator.Api.csproj).
- Additional reference: target `netcoreapp3.0` in [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](src/BotCreator.Api/BotCreator.Api.csproj); its [stable release was published on 2019-09-23](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

## Why this is worth exploring

Bot configuration connects to widget generation and reporting and event services. Following an operation from controller to service and SDK is the most useful route for understanding how a bot engine becomes a manageable product.

Code entry points:

- [`src/BotCreator.Api/Controllers/BotController.cs`](src/BotCreator.Api/Controllers/BotController.cs)
- [`src/BotCreator.Api/Controllers/WidgetConfigController.cs`](src/BotCreator.Api/Controllers/WidgetConfigController.cs)
- [`src/BotCreator.Service/Bot/EventService.cs`](src/BotCreator.Service/Bot/EventService.cs)

<!-- END project-context -->

## Structure and responsibilities

| Path | Responsibility |
| --- | --- |
| `src/BotCreator.Api/` | ASP.NET Core API: bots, users, authentication, reports, and widget configuration. |
| `src/BotCreator.Infrastructure/` | Entity Framework persistence, migrations, and JWT authentication. |
| `src/BotCreator.Model/, src/BotCreator.Service/` | Models and services; Mentor SDK, messaging, and email integration. |
| `src/BotCreator.Repository/` | Additional project targeting .NET Core 2.1. |
| `src/Functions/` | Azure Functions solution for metrics, feedback, dashboards, and other supporting tasks. |
| `src/BotCreator.Service.Test/, src/TestSdkConsoleApp/` | SDK tests and sample application. |

## Technologies and requirements

.NET Core 3.0 for the API, additional targets 2.1/2.2, and Azure Functions tools compatible with the project. SQL Server, Azure NoSQL storage, PubNub, SendGrid, and Mentor.telegram.bot.sdk packages are used according to configuration.

Targets read from project files: `netcoreapp2.1`, `netcoreapp2.2`, `netcoreapp3.0`.

## Setup and startup

Prepare databases and private configuration before starting BotCreator.Api. Functions have a separate solution at src/Functions/Functions.sln. Do not automatically run integration tests against the old connections.

Setup commands, run from this folder after reviewing private configuration:

```text
dotnet restore src/BotCreator.sln
dotnet build src/BotCreator.Api/BotCreator.Api.csproj
```

Solutions present: `src/BotCreator.sln`, `src/Functions/Functions.sln`.

## Configuration and integrations


## Status of this copy

Related to mentor.telegram.bot through its SDK and widget; the bot.shopifyapp Shopify integration also uses customer support widgets. A joint deployment was not verified.

The review was static: dependencies were not restored, and applications and tests were neither built nor run. The commands above are setup instructions, not validation results. Listed versions describe the preserved code.


## Public copy

Original credentials have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Local data, sessions, private certificates, and deployment profiles are omitted. Configure your own services and credentials before running. Building and operation with external services have not been validated.
