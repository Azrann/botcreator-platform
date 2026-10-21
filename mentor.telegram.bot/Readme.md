# mentor.telegram.bot

Plataforma de bots Telegram con flujos conversacionales, SDK, atención al cliente, suscripciones, integraciones de pagos y herramientas de administración.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

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
