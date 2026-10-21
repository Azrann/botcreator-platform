# BotCreator.UI

Frontend Angular del gestor de bots: acceso y registro de usuarios, recuperación de contraseña, edición de bots de atención al cliente, configuración del widget, informes, eventos y chat.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2019.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2019-11-07**, [`BotCreator.UI/tslint.json`](tslint.json); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2019-11-07**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [@angular/cli 8.1.3](https://www.npmjs.com/package/@angular/cli/v/8.1.3), publicada el **2019-07-29** (versión fijada en el lockfile), en [`BotCreator.UI/package.json`](package.json). Fecha contrastada con la versión de [`BotCreator.UI/package-lock.json`](package-lock.json).

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

## Por qué merece la pena mirarlo

La interfaz permite configurar un bot y probar su conversación desde el mismo panel. La conexión entre servicios HTTP, motor de chat y pantallas de prueba es interesante para estudiar cómo comprobar visualmente el comportamiento de una configuración.

Puntos de entrada al código:

- [`src/app/core`](src/app/core/)
- [`src/app/pages`](src/app/pages/)
- [`src/app/pages/bot/bot-test`](src/app/pages/bot/bot-test/)

<!-- END project-context -->

## Estructura y responsabilidades

| Ruta | Responsabilidad |
| --- | --- |
| `src/app/core/` | Servicios HTTP de autenticación, usuarios, bots e informes; middleware y motor de chat PubNub. |
| `src/app/pages/` | Acceso, registro, recuperación, lista/edición/prueba de bots, dashboard, eventos y perfil. |
| `projects/coda/` | Biblioteca y aplicación de componentes de panel: layouts, gráficos, mapas, formularios y widgets. |
| `src/environments/` | Entornos de la aplicación. |
| `angular.json, package.json` | Configuración del workspace y scripts de Angular CLI. |
| `firebase.json, .firebaserc` | Configuración de Firebase Hosting; .firebase contiene caché local. |

## Tecnologías y requisitos

Node/npm compatible con Angular CLI 8, Angular 8.1, RxJS 6 y NgRx 8. Las funciones de bots dependen del backend botcreator y de la configuración de mensajería. Firebase CLI solo hace falta para un despliegue expresamente autorizado.

## Preparación y arranque

Las fuentes src y projects/coda ya están presentes. Revisar las URLs del backend, instalar dependencias y usar npm start. npm run build y npm run prod-build generan dist; npm test, npm run lint y npm run e2e están declarados. El script deploy publica en Firebase y no se ejecutó.

Comandos de preparación, desde esta carpeta, después de revisar la configuración privada:

```text
npm install
npm start
```

## Configuración e integraciones

Revisar los servicios de src/app/core y los entornos de src/environments. ChatEngine contiene claves publish/subscribe de PubNub; Coda incluye una clave Google para mapas. Identificadores de Firebase y claves de navegador requieren revisar contexto y permisos; no equivalen por sí solos a una clave privada de cuenta.

## Estado de esta copia

Los servicios AuthService, BotService, ReportService y UserService corresponden a operaciones del backend botcreator. BotCreator.UI y botmanager.ui conservan casi las mismas fuentes; se encontraron diferencias en componentes de prueba de bots/eventos y recursos, por lo que se documentan como copias separadas. Se conserva la documentación original. Esta carpeta contiene 889 archivos de código/plantillas/estilos compartidos en rutas equivalentes con botmanager.ui; tres de ellos difieren en la comparación realizada.

La revisión fue estática: no se restauraron dependencias, no se compiló ni se ejecutaron aplicaciones o pruebas. Los comandos anteriores son instrucciones de preparación, no resultados de validación. Las versiones indicadas son las del código conservado.


## Copia pública

Las credenciales originales se han retirado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Los datos locales, sesiones, certificados privados y perfiles de despliegue se omiten. Configurar servicios y credenciales propios antes de ejecutar. La compilación y el funcionamiento con servicios externos no se han validado.
