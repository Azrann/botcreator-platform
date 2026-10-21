# botcreator

Backend para crear, desplegar y administrar bots de atención al cliente, sus widgets web, usuarios y métricas.

Documentación basada en los archivos locales revisados el 21 de octubre de 2026.

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
