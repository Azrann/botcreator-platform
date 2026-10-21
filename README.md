# botcreator-platform

Plataforma para crear y administrar bots, sus clientes Telegram, widgets e integración Shopify.

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
