# botcreator-platform

Plataforma para crear y administrar bots, sus clientes Telegram, widgets e integración Shopify.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2020.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2020-03-24**, [`botcreator/src/Functions/Functions/Functions/Status.cs`](botcreator/src/Functions/Functions/Functions/Status.cs); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro entre los repositorios Git originales incluidos: **2018-07-05**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [Microsoft.EntityFrameworkCore 3.1.2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.2), publicada el **2020-02-19** (versión declarada), en [`mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj`](mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj).
- Referencia adicional: destino `netcoreapp3.0` en [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](botcreator/src/BotCreator.Api/BotCreator.Api.csproj); su [versión estable se publicó el 2019-09-23](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

Años de referencia por componente (archivo más reciente datable):

| Componente | Año |
| --- | --- |
| [`botcreator`](botcreator/) | 2020 |
| [`mentor.telegram.bot`](mentor.telegram.bot/) | 2020 |
| [`bot.shopifyapp`](bot.shopifyapp/) | 2019 |
| [`BotCreator.UI`](BotCreator.UI/) | 2019 |
| [`botmanager.ui`](botmanager.ui/) | 2019 |
| [`MyPackages/BootstrapServices.cs`](examples/BootstrapServices.cs) | 2019 |

## Por qué merece la pena mirarlo

La combinación de motor conversacional, panel de configuración, widget e integración Shopify muestra cómo una biblioteca se convierte en un producto completo. El motor de acciones y las pantallas de prueba son los puntos de entrada más interesantes.

Puntos de entrada al código:

- [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Actions`](mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/)
- [`botcreator/src/BotCreator.Api/Controllers/WidgetConfigController.cs`](botcreator/src/BotCreator.Api/Controllers/WidgetConfigController.cs)
- [`BotCreator.UI/src/app/pages`](BotCreator.UI/src/app/pages/)

<!-- END project-context -->

## Contenido

| Directorio | Función |
| --- | --- |
| [`botcreator/`](botcreator/) | Backend para crear, desplegar y administrar bots de atención al cliente, sus widgets web, usuarios y métricas. |
| [`mentor.telegram.bot/`](mentor.telegram.bot/) | Plataforma de bots Telegram con flujos conversacionales, SDK, atención al cliente, suscripciones, integraciones de pagos y herramientas de administración. |
| [`bot.shopifyapp/`](bot.shopifyapp/) | Aplicación Shopify para instalar y configurar un widget de atención al cliente en el tema de una tienda. Incluye una interfaz integrada y un servidor que modifica los assets del tema. |
| [`BotCreator.UI/`](BotCreator.UI/) | Frontend Angular del gestor de bots: acceso y registro de usuarios, recuperación de contraseña, edición de bots de atención al cliente, configuración del widget, informes, eventos y chat. |
| [`botmanager.ui/`](botmanager.ui/) | Frontend Angular del gestor de bots: acceso y registro de usuarios, recuperación de contraseña, edición de bots de atención al cliente, configuración del widget, informes, eventos y chat. |

Se conservan las dos variantes de la interfaz, BotCreator.UI y botmanager.ui, para no perder sus diferencias. Los paquetes internos se deben reconstruir desde sus fuentes.

## Requisitos y preparación

.NET Core 2.1/2.2/3.0, Azure Functions, SQL Server, Angular 8, Node.js y SDK Mentor.

Esta es una copia histórica. Consultar los archivos de proyecto y los README de cada componente para sus versiones y dependencias. Restaurar NuGet/npm desde la carpeta correspondiente; las bibliotecas internas pueden necesitar reconstrucción. Preparar las bases de datos y los servicios externos con cuentas propias.

Soluciones conservadas:

- `botcreator/src/BotCreator.sln`
- `botcreator/src/Functions/Functions.sln`
- `mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sln`

Paquetes JavaScript principales:

- `BotCreator.UI/package.json`
- `botmanager.ui/package.json`
- `bot.shopifyapp/Chat-Customer-service/package.json`
- `bot.shopifyapp/node-server/package.json`
- `BotCreator.UI/projects/coda/package.json`
- `botmanager.ui/projects/coda/package.json`

## Configuración y seguridad

Los secretos originales se han eliminado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Buscar este marcador antes de ejecutar y aportar los valores mediante configuración local o variables de entorno. Las sesiones, claves privadas, certificados de firma, bases de datos, registros, perfiles de despliegue y paquetes binarios no se publican. El `.gitignore` protege estos archivos locales.

Se publica un historial nuevo: los commits antiguos permanecen únicamente en la copia local original. Los valores de ejemplo no permiten conectarse a los servicios originales.

## Estado de validación

Se realizó una revisión estática y un escaneo de secretos antes de publicar. No se ejecutaron aplicaciones, operaciones de trading, despliegues ni pruebas que contacten servicios externos. No se garantiza la compilación con herramientas actuales. Algunos proyectos necesitan dependencias históricas y activos que no están incluidos.

## Licencias y procedencia

Se conservan las licencias y atribuciones de terceros presentes en el código. No se añade una licencia global a los componentes que no la tenían. Esta importación conserva fuentes locales y no afirma ser una versión actual del proyecto original.
