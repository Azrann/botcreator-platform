**Languages:** [English](README.md) · [Español](README.es.md)

# botcreator-platform

Platform for creating and managing bots, Telegram clients, widgets, and Shopify integration.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2020.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2020-03-24**, [`botcreator/src/Functions/Functions/Functions/Status.cs`](botcreator/src/Functions/Functions/Functions/Status.cs); date of the commit modifying the file, recovered from local Git history.
- Earliest record among the original Git repositories included: **2018-07-05**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [Microsoft.EntityFrameworkCore 3.1.2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.2), published on **2020-02-19** (declared version), in [`mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj`](mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj).
- Additional reference: target `netcoreapp3.0` in [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](botcreator/src/BotCreator.Api/BotCreator.Api.csproj); its [stable release was published on 2019-09-23](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

Reference years by component (most recent datable file):

| Component | Year |
| --- | --- |
| [`botcreator`](botcreator/) | 2020 |
| [`mentor.telegram.bot`](mentor.telegram.bot/) | 2020 |
| [`bot.shopifyapp`](bot.shopifyapp/) | 2019 |
| [`BotCreator.UI`](BotCreator.UI/) | 2019 |
| [`botmanager.ui`](botmanager.ui/) | 2019 |
| [`MyPackages/BootstrapServices.cs`](examples/BootstrapServices.cs) | 2019 |

## Why this is worth exploring

The combination of conversational engine, configuration dashboard, widget, and Shopify integration shows how a library becomes a complete product. The action engine and testing screens are the most interesting entry points.

Code entry points:

- [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Actions`](mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/)
- [`botcreator/src/BotCreator.Api/Controllers/WidgetConfigController.cs`](botcreator/src/BotCreator.Api/Controllers/WidgetConfigController.cs)
- [`BotCreator.UI/src/app/pages`](BotCreator.UI/src/app/pages/)

<!-- END project-context -->

## Contents

| Directory | Function |
| --- | --- |
| [`botcreator/`](botcreator/) | Backend for creating, deploying, and managing customer support bots, web widgets, users, and metrics. |
| [`mentor.telegram.bot/`](mentor.telegram.bot/) | Telegram bot platform with conversational flows, SDK, customer support, subscriptions, payment integrations, and administration tools. |
| [`bot.shopifyapp/`](bot.shopifyapp/) | Shopify application for installing and configuring a customer support widget in a store's theme. Includes an embedded interface and a server that modifies theme assets. |
| [`BotCreator.UI/`](BotCreator.UI/) | Angular bot manager frontend: login and registration, password recovery, customer support bot editing, widget configuration, reports, events, and chat. |
| [`botmanager.ui/`](botmanager.ui/) | Angular bot manager frontend: login and registration, password recovery, customer support bot editing, widget configuration, reports, events, and chat. |

Both interface variants, BotCreator.UI and botmanager.ui, are retained to preserve their differences. Internal packages must be rebuilt from source.

## Requirements and setup

.NET Core 2.1/2.2/3.0, Azure Functions, SQL Server, Angular 8, Node.js, and the Mentor SDK.

This is a historical copy. Consult each component's project files and README for versions and dependencies. Restore NuGet/npm from the appropriate folder; internal libraries may need rebuilding. Prepare databases and external services using your own accounts.

Preserved solutions:

- `botcreator/src/BotCreator.sln`
- `botcreator/src/Functions/Functions.sln`
- `mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sln`

Main JavaScript packages:

- `BotCreator.UI/package.json`
- `botmanager.ui/package.json`
- `bot.shopifyapp/Chat-Customer-service/package.json`
- `bot.shopifyapp/node-server/package.json`
- `BotCreator.UI/projects/coda/package.json`
- `botmanager.ui/projects/coda/package.json`

## Configuration and security

Original secrets have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Search for this marker before running and provide values through local configuration or environment variables. Sessions, private keys, signing certificates, databases, logs, deployment profiles, and binary packages are not published. The `.gitignore` protects these local files.

A fresh history is published: old commits remain only in the original local copy. Example values cannot connect to the original services.

## Validation status

A static review and secret scan were performed before publication. Applications, trading operations, deployments, and tests contacting external services were not run. Building with current tools is not guaranteed. Some projects need historical dependencies and assets that are not included.

## Licenses and provenance

Third-party licenses and attributions present in the code are retained. No blanket license is added to components that lacked one. This import preserves local sources and does not claim to be a current version of the original project.
