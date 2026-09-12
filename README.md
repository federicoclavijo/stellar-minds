**STELLAR MINDS**

Proyecto final de la materia Desarrollo Web Asistido con IA — ORT Uruguay.

Aplicación web orientada a la gestión de un observatorio astronómico, incluyendo usuarios y roles, instrumentos de observación, préstamos, observaciones y funcionalidades de asistencia mediante inteligencia artificial.

Nota: Los datos marcados entre [ ] deben sustituirse por los valores correspondientes a este proyecto antes de publicar el README.

Índice

Descripción general

Demo publicada

Alcance y funcionalidades

Organización del repositorio

Arquitectura de la solución

Requisitos

Configuración y ejecución local

Base de datos

Configuración de la API

Ejecutar la API

Configurar el cliente MVC

Ejecutar el cliente

Comprobaciones rápidas

Ejecución con servicios publicados

Usuarios para pruebas

API REST

Tecnologías utilizadas

Consideraciones sobre seguridad

Aprendizajes

Autor

Descripción general

El proyecto consiste en una solución web dividida en un backend desarrollado con ASP.NET Core y un cliente MVC encargado de la interfaz de usuario.

La comunicación entre ambas partes se realiza mediante una API REST, permitiendo mantener separadas las responsabilidades de presentación, lógica de negocio y persistencia.

El sistema permite administrar diferentes elementos relacionados con la actividad de un observatorio, como:

Usuarios y sus respectivos roles.

Equipos e instrumentos astronómicos.

Préstamos de equipamiento.

Registro de observaciones.

Objetos celestes.

Consultas y operaciones relacionadas con las actividades del observatorio.

Auditoría de determinadas acciones.

Evaluación mediante IA de la compatibilidad entre determinados equipos y objetos celestes.

La solución también incorpora autenticación mediante JWT y documentación de los endpoints mediante Swagger / OpenAPI.

Demo publicada

El sistema cuenta con una versión desplegada para poder probar la aplicación sin necesidad de instalar todo el entorno de desarrollo.

Componente

Dirección

Cliente MVC

[URL_FRONTEND]

API REST

[URL_API]

Base de datos

[SERVIDOR_O_HOST_BD]

La versión publicada utiliza los servicios configurados para el entorno de despliegue, por lo que para realizar una prueba básica no es necesario levantar SQL Server, la API y el cliente de forma manual.

Si la demo no está disponible

El hosting utilizado puede presentar períodos de inactividad, límites del plan gratuito, mantenimiento u otros inconvenientes.

En ese caso, el proyecto puede ejecutarse completamente de forma local siguiendo los pasos incluidos en la sección Configuración y ejecución local.

Alcance y funcionalidades

El desarrollo reúne los principales conceptos trabajados durante la materia y los aplica dentro de una única solución.

Gestión de información

Se implementaron operaciones de alta, consulta, modificación y eliminación sobre los principales recursos del sistema, respetando las reglas de negocio definidas para cada caso.

Separación de responsabilidades

La aplicación evita concentrar toda la lógica en los controladores. Las operaciones se distribuyen entre las distintas capas, permitiendo que cada componente tenga una responsabilidad concreta.

Interfaces y abstracciones

El acceso a los datos y determinados servicios se realizan mediante contratos e interfaces. Esto permite desacoplar la lógica de las implementaciones específicas.

Inyección de dependencias

Los servicios y repositorios son registrados en el contenedor de dependencias de ASP.NET Core, facilitando su utilización desde los distintos componentes de la aplicación.

Seguridad y roles

El sistema utiliza autenticación basada en JSON Web Tokens (JWT). Dependiendo del rol del usuario autenticado, se habilitan o restringen determinadas operaciones.

Integración con inteligencia artificial

La aplicación incorpora una integración con una API de IA para resolver una funcionalidad específica del dominio astronómico. Esta integración se utiliza como un servicio complementario y no como sustituto de las operaciones principales del sistema.

API REST

El backend expone recursos mediante endpoints HTTP y puede ser consumido de manera independiente por el cliente MVC.

Organización del repositorio

La solución está organizada de manera que el backend y el cliente puedan desarrollarse y ejecutarse de forma independiente.

Una representación simplificada es:

[NOMBRE DEL REPOSITORIO]/
│
├── [PROYECTO BACKEND]/
│   ├── Dominio
│   ├── Excepciones
│   ├── Acceso a datos
│   ├── Lógica de aplicación
│   ├── DTOs
│   └── Web API
│
├── [PROYECTO CLIENTE]/
│   └── Aplicación MVC
│
└── [SCRIPT BASE DE DATOS].sql

Los nombres anteriores deben adaptarse a la estructura real de la solución.

Arquitectura de la solución

El backend sigue una organización por capas inspirada en Clean Architecture y Domain-Driven Design (DDD).

La idea principal es que las reglas importantes del sistema no dependan directamente de detalles externos como la base de datos o la interfaz.

Dominio

Contiene los conceptos principales del problema, como entidades, objetos de valor y contratos relacionados con el dominio.

Esta capa representa las reglas y estructuras fundamentales de la aplicación.

Excepciones

Agrupa las excepciones utilizadas para representar errores relacionados con reglas de negocio o situaciones inválidas detectadas durante la ejecución.

Acceso a datos

Esta parte contiene la implementación de persistencia mediante Entity Framework Core y SQL Server.

Aquí se encuentran elementos como:

DbContext.

Configuración de entidades.

Repositorios.

Consultas y operaciones sobre la base de datos.

Lógica de aplicación

Contiene los casos de uso de la aplicación.

Su objetivo es coordinar las operaciones necesarias para realizar acciones como crear, consultar, modificar o eliminar información sin trasladar estas responsabilidades directamente al controlador.

DTOs

Los Data Transfer Objects permiten definir qué información se intercambia entre las distintas capas y, especialmente, qué datos expone la API.

Esto evita depender directamente de las entidades de persistencia para todas las respuestas y solicitudes.

Web API

Es la puerta de entrada HTTP al backend.

En esta capa se encuentran:

Controllers.

Configuración de autenticación.

JWT.

Swagger.

Inyección de dependencias.

Configuración de servicios.

Endpoints REST.

Cliente MVC

El frontend está implementado utilizando ASP.NET MVC y consume la API REST.

De esta forma, la interfaz no necesita acceder directamente a SQL Server: todas las operaciones se realizan mediante las funcionalidades expuestas por el backend.

Requisitos

Para ejecutar el proyecto localmente se necesita contar con:

.NET SDK [VERSIÓN].

SQL Server, SQL Server Express o LocalDB.

Un entorno de desarrollo compatible, por ejemplo Visual Studio o Visual Studio Code.

Un cliente para administrar SQL Server, como SQL Server Management Studio, Azure Data Studio o equivalente.

Una API Key del proveedor de IA, únicamente si se desea utilizar la funcionalidad que depende de inteligencia artificial.

Antes de comenzar, se recomienda verificar la versión de .NET utilizada por la solución.

dotnet --version

Configuración y ejecución local

La ejecución local requiere levantar tres componentes:

Base de datos.

API REST.

Cliente MVC.

El orden recomendado es justamente ese, ya que el frontend depende de la API y la API depende de la base de datos.

1. Preparar la base de datos

Primero se debe disponer de una instancia local de SQL Server o LocalDB.

Crear la base

Si el proyecto requiere crear la base manualmente, utilizar el nombre configurado en la cadena de conexión:

CREATE DATABASE [NOMBRE_BASE_DATOS];

Si el proyecto ya utiliza migraciones que crean la base automáticamente, este paso puede no ser necesario.

Aplicar las migraciones

Desde la carpeta correspondiente al proyecto que utiliza Entity Framework Core se pueden aplicar las migraciones con:

dotnet restore
dotnet ef database update

Si la solución utiliza una separación entre el proyecto de inicio y el proyecto donde se encuentra el contexto de Entity Framework, utilizar los parámetros correspondientes, por ejemplo:

dotnet ef database update \
  --project [PROYECTO_ACCESO_DATOS] \
  --startup-project [PROYECTO_API]

Cargar datos iniciales

Si el repositorio incluye un script SQL con información de prueba, ejecutarlo sobre la base creada.

Por ejemplo:

[NOMBRE_SCRIPT].sql

Este archivo puede contener usuarios, equipos, objetos celestes y demás registros necesarios para probar el sistema.

Configuración de la conexión a SQL Server

La API debe conocer dónde se encuentra la base de datos.

Un ejemplo para LocalDB podría ser:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=[NOMBRE_BASE_DATOS];Trusted_Connection=True;TrustServerCertificate=True"
}

En SQL Server Express podría utilizarse una cadena similar a:

Server=localhost\SQLEXPRESS;Database=[NOMBRE_BASE_DATOS];Trusted_Connection=True;TrustServerCertificate=True

La cadena exacta debe adaptarse a la instalación utilizada.

2. Configurar el backend

La configuración de la API se encuentra normalmente en:

[PROYECTO_API]/appsettings.json

Para desarrollo local se recomienda utilizar appsettings.Development.json cuando sea posible, especialmente para valores que no deberían formar parte de la configuración pública.

Una configuración orientativa sería:

{
  "ConnectionStrings": {
    "DefaultConnection": "[CADENA_DE_CONEXION_LOCAL]"
  },
  "GeminiApiKey": "[API_KEY]",
  "SecretTokenKey": "[CLAVE_SECRETA_PARA_JWT]"
}

Los nombres deben coincidir exactamente con los utilizados por la aplicación.

Clave de inteligencia artificial

La API Key debe mantenerse fuera del repositorio público.

Para un proyecto publicado en GitHub, no se debe subir una clave real dentro de appsettings.json, código fuente u otro archivo versionado.

Una alternativa es utilizar:

User Secrets durante desarrollo.

Variables de entorno.

Secretos del servicio de hosting.

Configuración segura del proveedor de despliegue.

3. Iniciar la API

Ubicarse en el directorio del proyecto Web API:

cd [RUTA_PROYECTO_API]

Restaurar dependencias:

dotnet restore

Ejecutar la aplicación:

dotnet run

La terminal mostrará la dirección en la que quedó disponible el servidor.

Por ejemplo:

http://localhost:[PUERTO]

Swagger

Si Swagger está habilitado, se puede acceder mediante:

http://localhost:[PUERTO]/swagger

Desde allí se pueden visualizar y probar los endpoints disponibles en la API.

Una vez que la API esté funcionando, mantener esa ejecución activa mientras se inicia el cliente MVC.

4. Configurar el cliente MVC

El cliente debe conocer la dirección de la API que va a consumir.

El valor suele encontrarse en:

[PROYECTO_CLIENTE]/appsettings.json

Para trabajar completamente en local:

{
  "ApiBaseUrl": "http://localhost:[PUERTO_API]/"
}

La dirección debe coincidir con el puerto utilizado por la API.

Entorno publicado

Cuando el cliente se encuentra desplegado y la API también está publicada, el valor debe apuntar a la URL correspondiente al backend:

{
  "ApiBaseUrl": "[URL_API_PUBLICADA]/"
}

La configuración local y la de producción no tienen por qué utilizar la misma URL.

5. Iniciar el cliente

Abrir una segunda terminal y dirigirse al proyecto MVC:

cd "[RUTA_PROYECTO_CLIENTE]"

Restaurar los paquetes:

dotnet restore

Ejecutar:

dotnet run

Luego ingresar desde el navegador a la dirección indicada por ASP.NET Core.

Por ejemplo:

http://localhost:[PUERTO_CLIENTE]/

La ruta concreta del login dependerá de la estructura del proyecto, por ejemplo:

http://localhost:[PUERTO_CLIENTE]/Usuario/Login

Comprobaciones rápidas

Antes de considerar que el entorno está correctamente configurado, verificar:

SQL Server o LocalDB está ejecutándose.

La base de datos existe.

Las migraciones fueron aplicadas.

Los datos de prueba fueron cargados.

La cadena DefaultConnection apunta a la base correcta.

La API inicia sin errores.

Swagger responde correctamente.

ApiBaseUrl del MVC apunta a la API correcta.

El cliente MVC puede abrirse desde el navegador.

El login permite autenticarse con un usuario de prueba.

Las funcionalidades que dependen de IA tienen configurada su clave correspondiente.

Problemas habituales

Problema

Posible causa

La API no inicia

Error en appsettings, dependencias o configuración de SQL Server

No conecta con SQL Server

Cadena de conexión incorrecta o servicio detenido

La base no tiene datos

No se ejecutó el script de carga o las migraciones correspondientes

El login devuelve error

La API no está disponible o ApiBaseUrl es incorrecta

Swagger no aparece

Revisar configuración del proyecto y entorno de ejecución

La función de IA falla

API Key ausente, inválida o servicio externo no disponible

El puerto ya está ocupado

Cambiar el puerto en launchSettings.json o configuración equivalente

El frontend abre pero no muestra información

Revisar que pueda comunicarse correctamente con la API

Ejecución con servicios publicados

Cuando tanto el cliente como la API se encuentran desplegados, el usuario final puede utilizar la aplicación directamente desde el navegador.

La comunicación sigue el siguiente flujo:

Navegador
    │
    ▼
Cliente MVC publicado
    │
    ▼
API REST publicada
    │
    ├──► SQL Server
    │
    └──► Servicio de IA

Si solamente una parte de la solución está publicada, también es posible ejecutar la otra localmente siempre que las URLs y configuraciones permitan dicha comunicación.

Por ejemplo, un escenario híbrido podría ser:

Frontend local
      │
      ▼
API publicada
      │
      ▼
Base de datos publicada

La configuración concreta dependerá de las URLs y restricciones del entorno de despliegue.

Usuarios para pruebas

El proyecto incluye usuarios destinados a facilitar la comprobación de los distintos permisos del sistema.

Completar esta tabla con las credenciales reales incluidas en la base de datos de prueba:

Rol

Usuario

Contraseña

Uso principal

Administrador

[USUARIO_ADMIN]

[CONTRASEÑA]

Administración general

Coordinador

[USUARIO_COORD]

[CONTRASEÑA]

Gestión operativa

Socio

[USUARIO_SOCIO]

[CONTRASEÑA]

Operaciones disponibles para socios

Importante: si este repositorio es público, no conviene utilizar aquí contraseñas de cuentas reales o credenciales de producción. Las credenciales documentadas deben corresponder únicamente a usuarios de prueba.

API REST

El backend expone diferentes recursos mediante endpoints HTTP.

La tabla siguiente debe ajustarse a las rutas reales implementadas:

Recurso

Ruta base

Usuarios

/api/[usuario]

Equipos

/api/[equipo]

Préstamos

/api/[prestamo]

Observaciones

/api/[observacion]

Objetos celestes

/api/[objeto-celeste]

Auditoría

/api/[auditoria]

La API utiliza los verbos HTTP habituales según la operación:

GET para consultar información.

POST para crear recursos.

PUT o PATCH para modificar información, según la implementación.

DELETE para eliminar recursos cuando la regla de negocio lo permite.

Autenticación

El acceso a los recursos protegidos utiliza JWT Bearer Authentication.

El usuario se autentica mediante el endpoint de login y, si las credenciales son válidas, obtiene un token.

Conceptualmente:

POST /api/[usuario]/login
        │
        ▼
Credenciales válidas
        │
        ▼
Token JWT
        │
        ▼
Requests autenticados

Los endpoints que requieren autenticación deben recibir el token en el encabezado:

Authorization: Bearer [TOKEN]

Además de comprobar que el usuario esté autenticado, determinados endpoints pueden restringir el acceso de acuerdo con su rol.

Tecnologías utilizadas

La solución combina distintas herramientas y tecnologías:

Backend

C#.

ASP.NET Core.

ASP.NET Core Web API.

Entity Framework Core.

SQL Server.

JWT Bearer Authentication.

Cliente

ASP.NET MVC.

Razor Views.

HTML / CSS / JavaScript, según las funcionalidades implementadas.

Desarrollo y documentación

Swagger / OpenAPI.

Git y GitHub.

Herramientas de desarrollo de .NET.

Inteligencia artificial

API de [PROVEEDOR DE IA] para la funcionalidad relacionada con asistencia/evaluación astronómica.

Despliegue

[HOSTING / SERVICIO UTILIZADO].

Consideraciones sobre seguridad

Aunque el proyecto puede incluir configuraciones necesarias para una demostración académica, hay algunos aspectos importantes al publicar el repositorio.

No subir secretos

Nunca se deben incluir en Git:

API Keys reales.

Contraseñas de producción.

Claves privadas.

Tokens permanentes.

Cadenas de conexión que contengan credenciales reales.

En su lugar, se pueden utilizar variables de entorno o mecanismos específicos de gestión de secretos.

JWT

La clave utilizada para firmar los tokens debe ser suficientemente robusta y no debería ser una clave compartida públicamente en el repositorio.

Base de datos de demostración

Cuando la aplicación se encuentra publicada para una entrega o demostración, es recomendable que la base utilizada no contenga información personal o datos que no deberían quedar expuestos.

Aprendizajes

El desarrollo de este proyecto permitió integrar varios de los contenidos trabajados durante la carrera dentro de una aplicación completa.

Uno de los principales aprendizajes fue comprender la utilidad de dividir una aplicación en responsabilidades diferentes. Separar dominio, lógica, persistencia, API y presentación facilita el mantenimiento y evita que todo el código termine concentrado en los controladores o en las vistas.

La implementación de Clean Architecture y conceptos de DDD permitió llevar a la práctica ideas como entidades, interfaces, repositorios, casos de uso y reglas de negocio. Más allá de la estructura, esto ayudó a comprender cómo organizar un proyecto para que sus componentes tengan una dependencia controlada entre sí.

Otro punto importante fue trabajar con Entity Framework Core y SQL Server. Esto permitió conectar las reglas de la aplicación con una base de datos real, manejar relaciones entre entidades y utilizar migraciones para mantener sincronizado el modelo con la estructura persistida.

La creación de una API REST independiente del frontend también fue un aprendizaje relevante. El cliente MVC no accede directamente a la base de datos, sino que utiliza los endpoints del backend. Esto hace que ambas partes estén desacopladas y permite que la misma API pueda ser consumida por otros clientes en el futuro.

La incorporación de JWT permitió comprender mejor la diferencia entre autenticación y autorización. No alcanza con saber quién es el usuario: también es necesario determinar qué acciones puede realizar de acuerdo con su rol.

La integración de un servicio externo de inteligencia artificial agregó otro desafío, ya que implicó consumir una API externa y contemplar que un servicio de terceros puede fallar independientemente del funcionamiento del resto de la aplicación.

Finalmente, el despliegue del sistema permitió enfrentarse a problemas que no aparecen necesariamente durante el desarrollo local, como configuración de URLs, cadenas de conexión, variables de entorno, certificados, puertos y disponibilidad del hosting.

En conjunto, el proyecto permitió pasar de trabajar con funcionalidades aisladas a construir una solución web completa, donde diferentes tecnologías y capas deben funcionar coordinadamente para obtener un producto que pueda ser ejecutado y probado por otra persona.

Autor

[NOMBRE Y APELLIDO]
ORT Uruguay — [CARRERA / SEMESTRE]
