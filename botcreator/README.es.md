**Languages:** [English](Readme.md) · [Español](README.es.md)

# botcreator

Backend para crear, desplegar y administrar bots de atención al cliente, sus widgets web, usuarios y métricas.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

<!-- BEGIN project-context -->

## Antigüedad y evidencia

**Año de referencia de esta versión: 2020.** Se toma el archivo de código o configuración más reciente con una fecha histórica recuperable; no se ha podido confirmar la fecha exacta de creación.

- Archivo más reciente datable: **2020-03-24**, [`botcreator/src/Functions/Functions/Functions/Status.cs`](src/Functions/Functions/Functions/Status.cs); fecha del commit que modificó el archivo, recuperada del Git local.
- Primer registro del repositorio Git original que contiene este código: **2019-01-18**. Puede corresponder a una importación; no prueba cuándo se escribió por primera vez.
- Dependencia fechada más reciente entre las comprobadas: [Microsoft.Extensions.DependencyInjection 3.1.2](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/3.1.2), publicada el **2020-02-18** (versión declarada), en [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](src/BotCreator.Api/BotCreator.Api.csproj).
- Referencia adicional: destino `netcoreapp3.0` en [`botcreator/src/BotCreator.Api/BotCreator.Api.csproj`](src/BotCreator.Api/BotCreator.Api.csproj); su [versión estable se publicó el 2019-09-23](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core). Es una pista de generación tecnológica; pudieron existir versiones preliminares anteriores.

La publicación de una dependencia sitúa la configuración conservada a partir de esa versión, si la referencia es válida. Una actualización de paquetes puede ser mucho más reciente que el inicio del proyecto. No se restauraron paquetes para comprobarlo.

Se descartaron las marcas de tiempo de la copia de octubre de 2026 y los documentos generados durante esta revisión. Las fechas recuperables no garantizan que no existan cambios posteriores sin datar.

## Por qué merece la pena mirarlo

La configuración de bots se conecta con la generación del widget y con servicios de informes y eventos. La parte más aprovechable es seguir una operación desde el controlador hasta el servicio y el SDK, para entender cómo se convierte un motor de bots en un producto administrable.

Puntos de entrada al código:

- [`src/BotCreator.Api/Controllers/BotController.cs`](src/BotCreator.Api/Controllers/BotController.cs)
- [`src/BotCreator.Api/Controllers/WidgetConfigController.cs`](src/BotCreator.Api/Controllers/WidgetConfigController.cs)
- [`src/BotCreator.Service/Bot/EventService.cs`](src/BotCreator.Service/Bot/EventService.cs)

<!-- END project-context -->

## Estructura y responsabilidades

| Ruta | Responsabilidad |
| --- | --- |
| `src/BotCreator.Api/` | API ASP.NET Core: bots, usuarios, autenticación, informes y configuración del widget. |
| `src/BotCreator.Infrastructure/` | Persistencia Entity Framework, migraciones y autenticación JWT. |
| `src/BotCreator.Model/, src/BotCreator.Service/` | Modelos y servicios; integración con el SDK Mentor, mensajería y correo. |
| `src/BotCreator.Repository/` | Proyecto adicional con destino .NET Core 2.1. |
| `src/Functions/` | Solución de Azure Functions para métricas, feedback, dashboard y otras tareas auxiliares. |
| `src/BotCreator.Service.Test/, src/TestSdkConsoleApp/` | Pruebas y aplicación de ejemplo del SDK. |

## Tecnologías y requisitos

.NET Core 3.0 para la API, destinos adicionales 2.1/2.2 y herramientas Azure Functions compatibles con el proyecto. SQL Server, almacenamiento NoSQL de Azure, PubNub, SendGrid y paquetes Mentor.telegram.bot.sdk según la configuración.

Destinos leídos de los proyectos: `netcoreapp2.1`, `netcoreapp2.2`, `netcoreapp3.0`.

## Preparación y arranque

Preparar bases y configuración privada antes de iniciar BotCreator.Api. Las funciones tienen una solución separada en src/Functions/Functions.sln. No ejecutar automáticamente pruebas de integración con las conexiones antiguas.

Comandos de preparación, desde esta carpeta, después de revisar la configuración privada:

```text
dotnet restore src/BotCreator.sln
dotnet build src/BotCreator.Api/BotCreator.Api.csproj
```

Soluciones presentes: `src/BotCreator.sln`, `src/Functions/Functions.sln`.

## Configuración e integraciones


## Estado de esta copia

Relacionado con mentor.telegram.bot por su SDK y el widget; la integración Shopify de bot.shopifyapp también trabaja con widgets de atención. No se verificó un despliegue conjunto.

La revisión fue estática: no se restauraron dependencias, no se compiló ni se ejecutaron aplicaciones o pruebas. Los comandos anteriores son instrucciones de preparación, no resultados de validación. Las versiones indicadas son las del código conservado.


## Copia pública

Las credenciales originales se han retirado o sustituido por `REPLACE_WITH_YOUR_VALUE`. Los datos locales, sesiones, certificados privados y perfiles de despliegue se omiten. Configurar servicios y credenciales propios antes de ejecutar. La compilación y el funcionamiento con servicios externos no se han validado.
