**Languages:** [English](Readme.md) · [Español](README.es.md)

# mentor.telegram.bot

Telegram bot platform with conversational flows, SDK, customer support, subscriptions, payment integrations, and administration tools.

Documentation based on local files reviewed on 21 October 2026.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2020.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2020-02-27**, [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Bot.telegram.funnel.csproj`](src/Mentor.telegram.bot/Bot.telegram.funnel/Bot.telegram.funnel.csproj); date of the commit modifying the file, recovered from local Git history.
- First record in the original Git repository containing this code: **2018-07-05**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [Microsoft.EntityFrameworkCore 3.1.2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.2), published on **2020-02-19** (declared version), in [`mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj`](src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj).
- Additional reference: target `netcoreapp2.2` in [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.Configuration/Bot.Configuration.csproj`](src/Mentor.telegram.bot/Bot.Configuration/Bot.Configuration.csproj); its [stable release was published on 2018-12-04](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). This indicates the technology generation; earlier preview releases may have existed.

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

## Why this is worth exploring

The engine separates actions, processors, and interface abstractions to build conversational flows. Follow how the same conversational logic connects to clients, widgets, and payment or notification services.

Code entry points:

- [`src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/ActionProcessor.cs`](src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/ActionProcessor.cs)
- [`src/Mentor.telegram.bot/Bot.telegram.funnel/IUserInterfaceService.cs`](src/Mentor.telegram.bot/Bot.telegram.funnel/IUserInterfaceService.cs)
- [`src/Widget`](src/Widget/)

<!-- END project-context -->

## Structure and responsibilities

| Path | Responsibility |
| --- | --- |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.api/` | ASP.NET Core API and bot, configuration, and cron controllers. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.api.services/` | Bot initialization and chat logic. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.boundedContext/` | Domain, storage, email, and migrations. |
| `src/Mentor.telegram.bot/Bot.telegram.funnel/` | Flow engine, actions, and processors. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/` | SDK consumed by other projects such as botcreator. |
| `src/Mentor.telegram.bot/Mentor.telegram.client/, src/Mentor.telegram.bot/TelegramBot.SessionGenerator/` | MTProto/OpenTL client and session generation. |
| `src/Mentor.telegram.bot/Mentor.telegram.coinpayments/, src/Mentor.telegram.bot/Mentor.telegram.paypalpayments/, src/Mentor.telegram.bot/Mentor.telegram.blockchain/` | Payment collection and transaction query integrations. |
| `src/Mentor.telegram.bot/Mentor.telegram.cron/` | Recurring Hangfire tasks. |
| `src/Widget/, src/Wordpress plugin/` | Web widget and WordPress plugin. |

## Technologies and requirements

.NET Core 2.2, SQL Server, Azure storage, PubNub, Telegram, and the email/payment services selected by each bot. Also uses internal Core.Common and Mentor packages.

Targets read from project files: `netcoreapp2.2`.

## Setup and startup

Prepare private configuration and dependencies before starting the API or scheduler. Explicitly choose which bots and integrations to enable: initialization logic and credentials appear in code and tests.

Setup commands, run from this folder after reviewing private configuration:

```text
dotnet restore src/Mentor.telegram.bot/Mentor.telegram.bot.api/Mentor.telegram.bot.api.csproj
dotnet build src/Mentor.telegram.bot/Mentor.telegram.bot.api/Mentor.telegram.bot.api.csproj
```

Solutions present: `src/Mentor.telegram.bot/Mentor.telegram.bot.sln`.

## Configuration and integrations


## Status of this copy

Several tests call real services or initialize data. APIs, bots, schedulers, and integration tests were not run.

The review was static: dependencies were not restored, and applications and tests were neither built nor run. The commands above are setup instructions, not validation results. Listed versions describe the preserved code.


## Public copy

Original credentials have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Local data, sessions, private certificates, and deployment profiles are omitted. Configure your own services and credentials before running. Building and operation with external services have not been validated.
