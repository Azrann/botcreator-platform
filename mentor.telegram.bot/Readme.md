# mentor.telegram.bot

Plataforma de bots Telegram con flujos conversacionales, SDK, atención al cliente, suscripciones, integraciones de pagos y herramientas de administración.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2020.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2020-02-27**, [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.telegram.funnel/Bot.telegram.funnel.csproj`](src/Mentor.telegram.bot/Bot.telegram.funnel/Bot.telegram.funnel.csproj); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2018-07-05**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [Microsoft.EntityFrameworkCore 3.1.2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.2), publicada el **2020-02-19** (versión declarada), en [`mentor.telegram.bot/src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj`](src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/Mentor.telegram.bot.sdk.csproj).
- Referencia adicional: destino `netcoreapp2.2` en [`mentor.telegram.bot/src/Mentor.telegram.bot/Bot.Configuration/Bot.Configuration.csproj`](src/Mentor.telegram.bot/Bot.Configuration/Bot.Configuration.csproj); su [versión estable se publicó el 2018-12-04](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

## Por qué merece la pena mirarlo

El motor separa acciones, procesadores y abstracciones de interfaz para construir flujos conversacionales. Es interesante seguir cómo una misma lógica de conversación se conecta con clientes, widgets y servicios de pago o notificación.

Puntos de entrada al código:

- [`src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/ActionProcessor.cs`](src/Mentor.telegram.bot/Bot.telegram.funnel/Actions/ActionProcessor.cs)
- [`src/Mentor.telegram.bot/Bot.telegram.funnel/IUserInterfaceService.cs`](src/Mentor.telegram.bot/Bot.telegram.funnel/IUserInterfaceService.cs)
- [`src/Widget`](src/Widget/)

<!-- END project-context -->

## Estructura y responsabilidades

| Ruta | Responsabilidad |
| --- | --- |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.api/` | API ASP.NET Core y controladores de bots, configuración y cron. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.api.services/` | Inicialización de bots y lógica de chats. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.boundedContext/` | Dominio, almacenamiento, correo y migraciones. |
| `src/Mentor.telegram.bot/Bot.telegram.funnel/` | Motor de flujos, acciones y procesadores. |
| `src/Mentor.telegram.bot/Mentor.telegram.bot.sdk/` | SDK consumido por otros proyectos como botcreator. |
| `src/Mentor.telegram.bot/Mentor.telegram.client/, src/Mentor.telegram.bot/TelegramBot.SessionGenerator/` | Cliente MTProto/OpenTL y generación de sesiones. |
| `src/Mentor.telegram.bot/Mentor.telegram.coinpayments/, src/Mentor.telegram.bot/Mentor.telegram.paypalpayments/, src/Mentor.telegram.bot/Mentor.telegram.blockchain/` | Integraciones de cobros y consulta de transacciones. |
| `src/Mentor.telegram.bot/Mentor.telegram.cron/` | Tareas recurrentes con Hangfire. |
| `src/Widget/, src/Wordpress plugin/` | Widget web y complemento WordPress. |

## Tecnologías y requisitos

.NET Core 2.2, SQL Server, almacenamiento Azure, PubNub, Telegram y los servicios de correo/pago seleccionados por cada bot. También se usan paquetes Core.Common y Mentor propios.

Destinos leídos de los proyectos: `netcoreapp2.2`.

## Preparación y arranque

Preparar configuración privada y dependencias antes de iniciar la API o el planificador. Elegir de forma explícita qué bots e integraciones activar: hay inicializaciones y credenciales en código y pruebas.

Comandos de preparación, desde esta carpeta, después de revisar la configuración privada:

```text
dotnet restore src/Mentor.telegram.bot/Mentor.telegram.bot.api/Mentor.telegram.bot.api.csproj
dotnet build src/Mentor.telegram.bot/Mentor.telegram.bot.api/Mentor.telegram.bot.api.csproj
```

Soluciones presentes: `src/Mentor.telegram.bot/Mentor.telegram.bot.sln`.

## Configuración e integraciones


## Estado de esta copia

Varias pruebas llaman servicios reales o inicializan datos. No se ejecutaron API, bots, planificadores ni pruebas de integración.

La revisión fue estática: no se restauraron dependencias, no se compiló ni se ejecutaron aplicaciones o pruebas. Los comandos anteriores son instrucciones de preparación, no resultados de validación. Las versiones indicadas son las del código conservado.


## Copia pública

Las credenciales originales se han retirado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Los datos locales, sesiones, certificados privados y perfiles de despliegue se omiten. Configurar servicios y credenciales propios antes de ejecutar. La compilación y el funcionamiento con servicios externos no se han validado.
