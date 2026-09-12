# STELLAR MINDS

Proyecto final de la materia **Desarrollo Web Asistido con IA — ORT Uruguay**.

Sistema web para la gestión de un observatorio astronómico, compuesto por un cliente MVC, una API REST y una base de datos SQL Server.

---

## Índice

1. [Descripción general](#-descripción-general)
2. [Demo y despliegue](#-demo-y-despliegue)
   - [Disponibilidad de la demo](#disponibilidad-de-la-demo)
3. [Funcionalidades principales](#-funcionalidades-principales)
4. [Estructura del repositorio](#-estructura-del-repositorio)
   - [Capas del backend](#capas-del-backend)
5. [Requisitos](#-requisitos)
6. [Ejecución local](#-ejecución-local)
   - [Arquitectura de ejecución](#arquitectura-de-ejecución)
   - [1. Preparar la base de datos](#1-preparar-la-base-de-datos)
   - [2. Configurar el backend](#2-configurar-el-backend)
   - [3. Ejecutar la API](#3-ejecutar-la-api)
   - [4. Configurar el cliente MVC](#4-configurar-el-cliente-mvc)
   - [5. Ejecutar el cliente](#5-ejecutar-el-cliente)
   - [Verificación rápida](#verificación-rápida)
   - [Problemas frecuentes](#problemas-frecuentes)
7. [Ejecución híbrida](#-ejecución-híbrida)
8. [Usuarios de prueba](#-usuarios-de-prueba)
9. [API REST](#-api-rest)
10. [Tecnologías](#-tecnologías)
11. [Seguridad y configuración](#-seguridad-y-configuración)
12. [Aprendizajes](#-aprendizajes)
13. [Autor](#-autor)

---

## Descripción general

El proyecto consiste en una aplicación web destinada a centralizar la gestión de las actividades de un observatorio astronómico.

La solución está formada por:

- **Cliente MVC:** interfaz utilizada por los usuarios.
- **API REST:** recibe las solicitudes y ejecuta la lógica correspondiente.
- **Base de datos:** almacena la información persistente del sistema.
- **Servicio de inteligencia artificial:** utilizado para una funcionalidad específica de evaluación astronómica.

El frontend no accede directamente a la base de datos. La comunicación se realiza mediante la API REST, manteniendo separadas las responsabilidades de presentación, negocio y persistencia.

---

## Demo y despliegue

| Componente | URL / ubicación |
|---|---|
| **Frontend MVC** | `https://stellar-minds.somee.com/` |
| **API REST** | `http://ObligatorioAPI.somee.com` |
| **Base de datos** | `obligatoriodb.mssql.somee.com` |

La versión desplegada utiliza la configuración correspondiente al entorno de demo/producción y permite probar la aplicación sin levantar todos los servicios localmente.

### Disponibilidad de la demo

El servicio de hosting puede presentar períodos de inactividad, límites del plan utilizado o tareas de mantenimiento.

Si algún componente publicado no se encuentra disponible, se puede utilizar la configuración local explicada en [Ejecución local](#-ejecución-local).

---

## Funcionalidades principales

### Usuarios y roles

- Registro y gestión de usuarios.
- Autenticación mediante credenciales.
- Manejo de diferentes roles.
- Control de permisos.
- Autorización de operaciones desde el backend.

### Equipamiento

- Gestión de instrumentos astronómicos.
- Consulta de información de los equipos.
- Alta, modificación y eliminación según permisos.
- Asociación del equipamiento con préstamos y observaciones.

### Préstamos

- Registro de préstamos.
- Consulta de información.
- Gestión de estados y datos asociados.
- Restricción de determinadas operaciones según el rol.

### Observaciones

- Registro de observaciones astronómicas.
- Asociación entre usuarios, equipos y objetos celestes.
- Consulta de observaciones.
- Integración con la funcionalidad de inteligencia artificial.

### Objetos celestes

- Gestión de objetos observables.
- Consulta de información.
- Utilización dentro del registro de observaciones.

### Inteligencia artificial

El sistema incorpora una API externa de IA para evaluar la relación entre un equipo astronómico y un determinado objeto celeste.

La funcionalidad se integra como un servicio adicional dentro de la aplicación.

### Auditoría

Se registran determinadas acciones relevantes para disponer de información sobre operaciones realizadas en el sistema.

---

## Estructura del repositorio

La solución separa el backend del cliente web.

```text
stellar-minds/
│
├── Obligatorio Server/
│   ├── Lógica de negocio
│   ├── LógicaAccesoDatos
│   ├── LógicaAplicación
│   ├── DTOs
│   └── Web API
│
├── Obligatorio Cliente/
│   └── WebApp (MVC)
│
├── datos.sql
└── README.md
```

### Capas del backend

| Componente | Responsabilidad |
|---|---|
| **Lógica de negocio** | Entidades, objetos de valor, reglas y contratos del negocio. |
| **Lógica de Acceso de Datos** | Entity Framework Core, `DbContext` y repositorios. |
| **Lógica de aplicación** | Casos de uso y coordinación de operaciones. |
| **DTOs** | Objetos utilizados para transferir información entre capas. |
| **Web API** | Controllers, autenticación, autorización, Swagger y endpoints REST. |

Esta separación permite mantener responsabilidades independientes y facilita el mantenimiento del código.

---

## Requisitos

Para ejecutar el proyecto localmente se necesita:

- **.NET SDK [VERSIÓN].**
- **SQL Server**, SQL Server Express, Developer o LocalDB.
- Visual Studio, Visual Studio Code u otro entorno compatible.
- Un cliente para administrar SQL Server:
  - SQL Server Management Studio.
  - Azure Data Studio.
  - `sqlcmd`.
- **API Key del servicio de IA**, únicamente para la funcionalidad que dependa de dicho servicio.
- Git, si se desea clonar el repositorio.

Para comprobar la versión instalada:

```bash
dotnet --version
```

---

# Ejecución local

La ejecución local permite utilizar todos los componentes sin depender del servidor de despliegue.

Orden recomendado:

```text
1. Base de datos
      ↓
2. API REST
      ↓
3. Cliente MVC
```

## Arquitectura de ejecución

```text
┌──────────────────────┐
│      Navegador       │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│     Cliente MVC      │
└──────────┬───────────┘
           │ HTTP
           ▼
┌──────────────────────┐
│       API REST       │
└───────┬─────────┬────┘
        │         │
        ▼         ▼
┌────────────┐  ┌──────────────┐
│ SQL Server │  │ Servicio IA  │
└────────────┘  └──────────────┘
```

---

## 1. Preparar la base de datos

### 1.1 Verificar SQL Server

Asegurarse de que SQL Server o LocalDB se encuentre instalado y funcionando.

### 1.2 Crear la base

Si es necesario crearla manualmente:

```sql
CREATE DATABASE [NOMBRE_BASE_DATOS];
```

### 1.3 Aplicar migraciones

Desde la ubicación correspondiente del backend:

```bash
dotnet restore
dotnet ef database update
```

Si la solución separa el proyecto de persistencia del proyecto de inicio:

```bash
dotnet ef database update   --project [PROYECTO_ACCESO_DATOS]   --startup-project [PROYECTO_API]
```

### 1.4 Cargar datos iniciales

Si el repositorio contiene un script SQL, ejecutarlo sobre la base creada.

Por ejemplo:

```text
datos.sql
```

El script puede incluir:

- Usuarios.
- Roles.
- Equipos.
- Objetos celestes.
- Préstamos.
- Observaciones.
- Otros datos necesarios para las pruebas.

### Cadenas de conexión

**LocalDB:**

```text
Server=(localdb)\mssqllocaldb;Database=[NOMBRE_BASE_DATOS];Trusted_Connection=True;TrustServerCertificate=True
```

**SQL Server Express:**

```text
Server=localhost\SQLEXPRESS;Database=[NOMBRE_BASE_DATOS];Trusted_Connection=True;TrustServerCertificate=True
```

**SQL Server con credenciales:**

```text
Server=localhost,1433;Database=[NOMBRE_BASE_DATOS];User Id=sa;Password=[PASSWORD];TrustServerCertificate=True
```

---

## 2. Configurar el backend

La configuración suele encontrarse en:

```text
[PROYECTO_API]/appsettings.json
```

Para desarrollo se recomienda utilizar:

```text
appsettings.Development.json
```

Ejemplo:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "[CADENA_DE_CONEXION]"
  },
  "SecretTokenKey": "[CLAVE_SECRETA]"
}
```

| Propiedad | Función |
|---|---|
| `DefaultConnection` | Conexión con SQL Server. |
| `apiKey` | API Key para la integración de IA. |
| `SecretTokenKey` | Clave utilizada para firmar tokens JWT. |


---

## 3. Ejecutar la API

Ingresar al directorio de la Web API:

```bash
cd [RUTA_PROYECTO_API]
```

Restaurar dependencias:

```bash
dotnet restore
```

Ejecutar:

```bash
dotnet run
```

O, si existe un perfil HTTP específico:

```bash
dotnet run --launch-profile http
```

Comprobar Swagger:

```text
http://localhost:[PUERTO_API]/swagger
```

Si Swagger carga correctamente, la API se encuentra disponible.


---

## 4. Configurar el cliente MVC

El cliente necesita conocer la dirección de la API.

Archivo:

```text
[PROYECTO_CLIENTE]/appsettings.json
```

Para una ejecución completamente local:

```json
{
  "ApiBaseUrl": "http://localhost:[PUERTO_API]/"
}
```

Para utilizar una API publicada:

```json
{
  "ApiBaseUrl": "[URL_API_PUBLICADA]/"
}
```

| `ApiBaseUrl` | Uso |
|---|---|
| `http://localhost:[PUERTO_API]/` | API ejecutándose localmente. |
| `[URL_API_PUBLICADA]/` | API alojada externamente. |

---

## 5. Ejecutar el cliente

Abrir otra terminal:

```bash
cd "[RUTA_PROYECTO_CLIENTE]"
```

Restaurar:

```bash
dotnet restore
```

Ejecutar:

```bash
dotnet run
```

Ingresar a la URL indicada por ASP.NET Core:

```text
http://localhost:[PUERTO_MVC]
```

Ejemplo de rutas:

```text
http://localhost:[PUERTO_MVC]/
http://localhost:[PUERTO_MVC]/[RUTA_LOGIN]
```

---

## Verificación rápida

- [ ] SQL Server / LocalDB está funcionando.
- [ ] La base de datos fue creada.
- [ ] Las migraciones fueron ejecutadas.
- [ ] Los datos iniciales fueron cargados.
- [ ] La conexión apunta a la base correcta.
- [ ] La API inicia correctamente.
- [ ] Swagger responde.
- [ ] `apiKey` apunta a la API correcta.
- [ ] El frontend inicia.
- [ ] El login funciona.
- [ ] La API Key está configurada si se desea utilizar IA.

---

## Problemas frecuentes

| Problema | Qué revisar |
|---|---|
| **La API no inicia** | `appsettings`, versión de .NET, dependencias y SQL Server. |
| **Error de conexión a SQL** | Servidor, instancia, base de datos y credenciales. |
| **La base está vacía** | Migraciones y script de datos iniciales. |
| **El login falla** | API activa y `ApiBaseUrl` correcto. |
| **Swagger no carga** | URL, puerto y configuración del proyecto. |
| **La IA no responde** | API Key y disponibilidad del servicio externo. |
| **Puerto ocupado** | Cambiarlo en `launchSettings.json` o configuración equivalente. |
| **Frontend sin información** | URL de la API y comunicación entre proyectos. |
| **CORS** | Revisar la política configurada en la API. |

---

# Ejecución híbrida

También es posible combinar componentes locales y publicados.

```text
Frontend local
      │
      ▼
API publicada
      │
      ▼
Base de datos publicada
```

Pasos generales:

1. Configurar el cliente MVC con la URL pública de la API.
2. Ejecutar el frontend localmente.
3. Verificar que la API remota esté disponible.
4. Comprobar que el servidor permita las solicitudes necesarias.

Este escenario puede resultar útil para realizar pruebas sobre el frontend sin levantar todo el backend local.

---

# Usuarios de prueba

Completar con las credenciales reales de la base de datos de desarrollo/demo:

| Rol | Usuario | Contraseña | Descripción |
|---|---|---|---|
| **Administrador** | `admin1` | `Admin123!` | Funciones administrativas. |
| **Coordinador** | `coord1` | `Coord123!` | Gestión operativa. |
| **Socio** | `socio1` | `Socio123!` | Funciones correspondientes al socio. |


---

# API REST

La API constituye el punto de comunicación entre el cliente y el backend.

| Recurso | Endpoint base |
|---|---|
| Usuarios | `/api/usuario` |
| Equipos | `/api/equipo` |
| Préstamos | `/api/prestamo` |
| Observaciones | `/api/observacion` |
| Objetos celestes | `/api/objetoceleste` |

### Métodos HTTP

| Método | Uso |
|---|---|
| `GET` | Consultar información. |
| `POST` | Crear recursos. |
| `PUT` | Actualizar información. |
| `DELETE` | Eliminar recursos cuando corresponda. |

---

## Autenticación mediante JWT

Las operaciones protegidas utilizan **JWT Bearer Authentication**.

Flujo general:

```text
Usuario
   │
   ▼
Login
   │
   ▼
Validación de credenciales
   │
   ▼
Token JWT
   │
   ▼
Solicitud autenticada
```

Las solicitudes protegidas utilizan normalmente:

```http
Authorization: Bearer [TOKEN]
```

El backend también puede verificar el rol del usuario antes de permitir determinadas operaciones.

---

## Swagger / OpenAPI

Swagger permite consultar y probar la API de forma interactiva.

Entre otras cosas permite:

- Visualizar endpoints.
- Consultar parámetros.
- Revisar modelos.
- Ejecutar solicitudes.
- Probar respuestas.
- Trabajar con endpoints protegidos.

URL local:

```text
http://localhost:[PUERTO_API]/swagger
```

---

# Tecnologías

| Tecnología | Utilización |
|---|---|
| **C#** | Backend. |
| **ASP.NET Core** | API y servicios web. |
| **ASP.NET MVC** | Cliente web. |
| **Entity Framework Core** | Persistencia de datos. |
| **SQL Server** | Base de datos. |
| **JWT Bearer** | Autenticación y autorización. |
| **Swagger / OpenAPI** | Documentación de API. |
| **HTML / CSS / JavaScript** | Interfaz web. |
| **Gemini IA** | Funcionalidad de inteligencia artificial. |
| **Git / GitHub** | Control de versiones. |
| **Somee** | Despliegue. |

---

# Seguridad y configuración

Aunque el proyecto está orientado a una entrega académica, se deben considerar algunas buenas prácticas.

### No publicar secretos

No incluir en Git:

- API Keys reales.
- Contraseñas de producción.
- Tokens permanentes.
- Claves privadas.
- Cadenas de conexión con credenciales reales.

Se pueden utilizar:

- `appsettings.Development.json`.
- User Secrets.
- Variables de entorno.
- Secretos del proveedor de hosting.

### JWT

La clave utilizada para firmar tokens debe mantenerse privada.

### Base de datos

La base utilizada para la demo debería contener únicamente información apropiada para un entorno académico y de pruebas.

---

# Aprendizajes

El desarrollo del proyecto permitió integrar diferentes conocimientos de programación y desarrollo web dentro de una aplicación completa.

Uno de los principales aprendizajes fue comprender la utilidad de una arquitectura dividida en capas. Separar dominio, aplicación, persistencia, API y presentación permite mantener responsabilidades claras y facilita futuras modificaciones.

El uso de **interfaces, repositorios e inyección de dependencias** permitió aplicar conceptos de SOLID, inversión de dependencias y desacoplamiento en un contexto práctico.

El trabajo con **Entity Framework Core y SQL Server** permitió profundizar en la persistencia, relaciones entre entidades y migraciones.

La construcción de una **API REST independiente del frontend** ayudó a comprender cómo diferentes componentes pueden comunicarse mediante HTTP sin que el cliente tenga acceso directo a la base de datos.

La implementación de **JWT** permitió trabajar tanto con autenticación como con autorización, diferenciando las operaciones disponibles según el rol del usuario.

La integración de **inteligencia artificial** agregó la experiencia de consumir un servicio externo y contemplar que este puede fallar de manera independiente al resto de la aplicación.

Por último, el despliegue permitió conocer problemas y configuraciones que no siempre aparecen durante el desarrollo local, como URLs, puertos, conexiones, variables de entorno y disponibilidad del hosting.

En conjunto, el proyecto permitió trabajar el ciclo completo de una aplicación web: modelado, persistencia, lógica, API, interfaz, autenticación, integración con servicios externos, documentación y despliegue.

---

# Autor

**Federico Clavijo**

**ORT Uruguay — Analista en Tecnologías de la Información (IT Analyst) — 3er semestre**
