**Languages:** [English](Readme.md) · [Español](README.es.md)

# bot.shopifyapp

Shopify application for installing and configuring a customer support widget in a store's theme. Includes an embedded interface and a server that modifies theme assets.

Documentation based on local files reviewed on 21 October 2026.

<!-- BEGIN project-context -->

## Historical dates and evidence

**Reference year for this version: 2019.** This uses the most recent source or configuration file with a recoverable historical date; the exact creation date could not be confirmed.

- Most recent datable file: **2019-04-03**, [`bot.shopifyapp/node-server/package-lock.json`](node-server/package-lock.json); date of the commit modifying the file, recovered from local Git history.
- First record in the original Git repository containing this code: **2019-03-15**. This may be an import and does not establish when the code was first written.
- Most recently published dependency among those checked: [@shopify/koa-shopify-auth 3.1.18](https://www.npmjs.com/package/@shopify/koa-shopify-auth/v/3.1.18), published on **2019-03-14** (version pinned in the lockfile), in [`bot.shopifyapp/Chat-Customer-service/package.json`](Chat-Customer-service/package.json). Cross-checked against the version in [`bot.shopifyapp/Chat-Customer-service/package-lock.json`](Chat-Customer-service/package-lock.json).

A dependency's publication places the preserved configuration at or after that version, if the reference is valid. Package updates may be much newer than the project's beginnings. Packages were not restored to verify this.

October 2026 copy timestamps and documents generated during this review were excluded. Recoverable dates do not guarantee that there are no later, undated changes.

## Why this is worth exploring

The flow from store authentication to asset modification connects OAuth, a Node server, and widget installation. It is useful for understanding how an external product integrates into a commerce platform.

Code entry points:

- [`Chat-Customer-service/server.js`](Chat-Customer-service/server.js)
- [`node-server/app.js`](node-server/app.js)

<!-- END project-context -->

## Structure and responsibilities

| Path | Responsibility |
| --- | --- |
| `Chat-Customer-service/` | Next.js 8, React 16, Polaris, and Koa; server.js implements Shopify OAuth. |
| `Chat-Customer-service/pages/` | Widget installation and configuration screens. |
| `node-server/` | Express server in app.js, MongoDB persistence, and Shopify calls to read and modify themes. |

## Technologies and requirements

Node/npm compatible with the historical dependencies, a Shopify application, a development store, MongoDB, and local TLS certificates. Both servers load certificates from local paths.

## Setup and startup

Set up the auxiliary service in node-server using npm install and node app.js. The code configures HTTPS on ports 443 and 5000; check certificate paths, permissions, and URLs for both processes.

Setup commands, run from this folder after reviewing private configuration:

```text
cd Chat-Customer-service
npm install
npm run dev
```

## Configuration and integrations

server.js reads SHOPIFY_API_KEY, SHOPIFY_API_SECRET_KEY, PORT, and NODE_ENV. Both db.js files contain private MongoDB connections. Adapt callback URLs, CORS, and backend settings to your own environment.

## Status of this copy

Theme operations modify the store. Some code stores or transmits Shopify tokens and requires review before use with a real store. Both packages' test scripts are placeholders that return an error.

The review was static: dependencies were not restored, and applications and tests were neither built nor run. The commands above are setup instructions, not validation results. Listed versions describe the preserved code.


## Public copy

Original credentials have been removed or replaced with `REPLACE_WITH_YOUR_VALUE`. Local data, sessions, private certificates, and deployment profiles are omitted. Configure your own services and credentials before running. Building and operation with external services have not been validated.
