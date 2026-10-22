**Languages:** [English](README.md) · [Español](README.es.md)

# botmanager.ui

Angular bot manager frontend: login and registration, password recovery, customer support bot editing, widget configuration, reports, events, and chat.

Documentation based on local files reviewed on 21 October 2026.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2019.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2019-11-09**, [`botmanager.ui/src/app/pages/events/events.component.ts`](src/app/pages/events/events.component.ts); preserved modification timestamp.
- First record in the original Git repository containing this code: **2019-01-13**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [@angular/core 8.1.3](https://www.npmjs.com/package/@angular/core/v/8.1.3), published on **2019-07-26** (version pinned in the lockfile), in [`botmanager.ui/package.json`](package.json). Cross-checked against the version in [`botmanager.ui/package-lock.json`](package-lock.json).

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

## Why this is worth exploring

This variant combines bot editing, conversation testing, and event inspection. Comparing it with BotCreator.UI reveals the evolution of an Angular dashboard on a shared base and the changes to testing and event screens.

Code entry points:

- [`src/app/pages/events/events.component.ts`](src/app/pages/events/events.component.ts)
- [`src/app/core`](src/app/core/)
- [`src/app/pages`](src/app/pages/)

<!-- END project-context -->

## Structure and responsibilities

| Path | Responsibility |
| --- | --- |
| `src/app/core/` | HTTP services for authentication, users, bots, and reports; middleware and PubNub chat engine. |
| `src/app/pages/` | Login, registration, recovery, bot listing/editing/testing, dashboard, events, and profile. |
| `projects/coda/` | Dashboard component library and application: layouts, charts, maps, forms, and widgets. |
| `src/environments/` | Application environments. |
| `angular.json, package.json` | Workspace configuration and Angular CLI scripts. |
| `firebase.json, .firebaserc` | Firebase Hosting configuration; .firebase contains local cache. |

## Technologies and requirements

Node/npm compatible with Angular CLI 8, Angular 8.1, RxJS 6, and NgRx 8. Bot features depend on the botcreator backend and messaging configuration. Firebase CLI is only needed for an explicitly authorized deployment.

## Setup and startup

The src and projects/coda sources are already present. Review backend URLs, install dependencies, and use npm start. npm run build and npm run prod-build generate dist; npm test, npm run lint, and npm run e2e are declared. The deploy script publishes to Firebase and was not run.

Setup commands, run from this folder after reviewing private configuration:

```text
npm install
npm start
```

## Configuration and integrations

Review services in src/app/core and environments in src/environments. ChatEngine contains PubNub publish/subscribe keys; Coda includes a Google Maps key. Firebase identifiers and browser keys require contextual and permission review; they are not, by themselves, private account keys.

## Status of this copy

AuthService, BotService, ReportService, and UserService correspond to botcreator backend operations. BotCreator.UI and botmanager.ui preserve nearly identical sources; differences were found in bot testing/event components and resources, so they are documented separately. Original documentation is retained.

The review was static: dependencies were not restored, and applications and tests were neither built nor run. The commands above are setup instructions, not validation results. Listed versions describe the preserved code.


## Public copy

Original credentials have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Local data, sessions, private certificates, and deployment profiles are omitted. Configure your own services and credentials before running. Building and operation with external services have not been validated.
