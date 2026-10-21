# bot.shopifyapp

Aplicación Shopify para instalar y configurar un widget de atención al cliente en el tema de una tienda. Incluye una interfaz integrada y un servidor que modifica los assets del tema.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2019.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2019-04-03**, [`bot.shopifyapp/node-server/package-lock.json`](node-server/package-lock.json); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2019-03-15**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [@shopify/koa-shopify-auth 3.1.18](https://www.npmjs.com/package/@shopify/koa-shopify-auth/v/3.1.18), publicada el **2019-03-14** (versión fijada en el lockfile), en [`bot.shopifyapp/Chat-Customer-service/package.json`](Chat-Customer-service/package.json). Fecha contrastada con la versión de [`bot.shopifyapp/Chat-Customer-service/package-lock.json`](Chat-Customer-service/package-lock.json).

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

## Por qué merece la pena mirarlo

El recorrido desde la autenticación de una tienda hasta la modificación de sus assets conecta OAuth, un servidor Node y la instalación de un widget. Es útil para entender la integración de un producto externo dentro de una plataforma de comercio.

Puntos de entrada al código:

- [`Chat-Customer-service/server.js`](Chat-Customer-service/server.js)
- [`node-server/app.js`](node-server/app.js)

<!-- END project-context -->

## Estructura y responsabilidades

| Ruta | Responsabilidad |
| --- | --- |
| `Chat-Customer-service/` | Next.js 8, React 16, Polaris y Koa; server.js implementa OAuth de Shopify. |
| `Chat-Customer-service/pages/` | Pantallas de instalación y configuración del widget. |
| `node-server/` | Servidor Express en app.js, persistencia MongoDB y llamadas a Shopify para leer y modificar temas. |

## Tecnologías y requisitos

Node/npm compatible con las dependencias históricas, una aplicación Shopify, una tienda de desarrollo, MongoDB y certificados TLS locales. Ambos servidores leen certificados desde rutas locales.

## Preparación y arranque

El servicio auxiliar se prepara en node-server con npm install y node app.js. El código configura HTTPS en los puertos 443 y 5000; comprobar rutas de certificados, permisos y URLs de ambos procesos.

Comandos de preparación, desde esta carpeta, después de revisar la configuración privada:

```text
cd Chat-Customer-service
npm install
npm run dev
```

## Configuración e integraciones

server.js lee SHOPIFY_API_KEY, SHOPIFY_API_SECRET_KEY, PORT y NODE_ENV. Los dos archivos db.js contienen conexiones MongoDB privadas. Adaptar las URLs de callback, CORS y backend a un entorno propio.

## Estado de esta copia

Las operaciones sobre temas modifican la tienda. Hay código que almacena o transmite tokens Shopify y requiere revisión antes de usarlo con una tienda real. El script test de ambos paquetes es un marcador que devuelve error.

La revisión fue estática: no se restauraron dependencias, no se compiló ni se ejecutaron aplicaciones o pruebas. Los comandos anteriores son instrucciones de preparación, no resultados de validación. Las versiones indicadas son las del código conservado.


## Copia pública

Las credenciales originales se han retirado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Los datos locales, sesiones, certificados privados y perfiles de despliegue se omiten. Configurar servicios y credenciales propios antes de ejecutar. La compilación y el funcionamiento con servicios externos no se han validado.
