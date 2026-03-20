# QuickHealth API 🏥

Sistema de registro de pacientes y triaje para el área de urgencias hospitalarias, desarrollado con **ASP.NET Core 10** siguiendo **Arquitectura Hexagonal**.

---

## 📋 Descripción

QuickHealth permite gestionar pacientes en urgencias. Al registrar un paciente, el sistema calcula automáticamente su **nivel de prioridad del 1 al 5** basado en sus signos vitales (frecuencia cardíaca, temperatura y presión arterial). El algoritmo de triaje está encapsulado en el dominio y no tiene dependencias externas.

| Nivel | Descripción |
|-------|-------------|
| 1 | Inmediato |
| 2 | Muy urgente |
| 3 | Urgente |
| 4 | Menos urgente |
| 5 | No urgente |

---

## 🏗️ Arquitectura

El proyecto sigue estrictamente la **Arquitectura Hexagonal**:

```
QuickHealth/
├── Domain/                         # Núcleo del negocio, sin dependencias externas
│   ├── Model/                      # Entidad Paciente
│   ├── Service/                    # CalculadoraTriaje (algoritmo de triaje)
│   └── Exception/                  # ExcepcionNegocio
│
├── Application/                    # Orquestación de casos de uso
│   ├── Ports/
│   │   ├── In/                     # ICasosUsoPaciente (puerto de entrada)
│   │   └── Out/                    # IRepositorioPaciente (puerto de salida)
│   ├── UseCases/                   # ServicioPaciente
│   ├── Mappings/                   # Configuración de Mapster
│   └── Validators/                 # Validaciones con FluentValidation
│
└── Infrastructure/                 # Todo lo externo
    └── Adapters/
        ├── In.Rest/
        │   └── Controllers/        # PacienteController
        └── Out.Persistence/
            ├── Context/            # QuickHealthDbContext
            ├── Repositories/       # RepositorioPaciente (SQL Server)
            │                       # RepositorioPacienteMemoria (In-Memory)
            └── Migrations/
    ├── DTOs/                       # Objetos de transferencia        
```

### Patrones de diseño aplicados

- **Strategy** — `CalculadoraTriaje` evalúa cada signo vital de forma independiente y toma el nivel más crítico.
- **Adapter** — `RepositorioPaciente` adapta EF Core al puerto `IRepositorioPaciente` sin que el dominio sepa que existe.
- **Repository** — abstracción del acceso a datos mediante `IRepositorioPaciente`.

---

## ⚙️ Requisitos

- [.NET 10]
- [SQL Server](Si se desea correr el repositorio en BD)
- [Visual Studio 2022-2025]

---

## 🚀 Configuración y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/samuelosorio-dev/QuickHealth.git
cd QuickHealth
```

### 2. Configurar la cadena de conexión

Abre el archivo `appsettings.json` y reemplaza `TU_SERVIDOR` con el nombre de tu instancia de SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=QuickHealth;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

Ejemplos comunes de servidor:
- LocalDB: `(localdb)\MSSQLLocalDB`
- SQL Server local: `localhost` o `.\SQLEXPRESS`

### 3. Aplicar las migraciones

Abre la **Package Manager Console** en Visual Studio (**Herramientas > NuGet Package Manager > Package Manager Console**) y ejecuta el comando:

```
Update-Database
```

Esto crea la base de datos `QuickHealth` con la tabla `Pacientes` automáticamente en SQL server.

### 4. Ejecutar el proyecto

Presiona `F5` en Visual Studio o ejecuta:

```bash
dotnet run
```

La API estará disponible en:
- **Swagger UI**: `https://localhost:7291`
- **HTTP**: `http://localhost:5239`

---

## 📡 Endpoints y comandos cURL

### ➕ Crear paciente
**POST** `/api/pacientes`

El sistema calcula automáticamente el nivel de prioridad según los signos vitales enviados.

```bash
curl -X POST https://localhost:7291/api/pacientes \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan Pérez",
    "edad": 45,
    "documento": "123456789",
    "frecuenciaCardiaca": 130,
    "temperatura": 39.8,
    "presionSistolica": 165,
    "presionDiastolica": 105
  }'
```

**Respuesta exitosa `201 Created`:**
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "edad": 45,
  "documento": "123456789",
  "frecuenciaCardiaca": 130,
  "temperatura": 39.8,
  "presionSistolica": 165,
  "presionDiastolica": 105,
  "nivelPrioridad": 2,
  "descripcionPrioridad": "Muy urgente",
  "fechaRegistro": "2026-03-12T10:00:00Z"
}
```

---

### 📋 Obtener todos los pacientes
**GET** `/api/pacientes/buscarTodos`

```bash
curl -X GET https://localhost:7291/api/pacientes/buscarTodos
```

---

### 🔍 Obtener paciente por ID
**GET** `/api/pacientes/{id}`

```bash
curl -X GET https://localhost:7291/api/pacientes/1
```

**Respuesta exitosa `200 OK`:**
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "edad": 45,
  "documento": "123456789",
  "frecuenciaCardiaca": 130,
  "temperatura": 39.8,
  "presionSistolica": 165,
  "presionDiastolica": 105,
  "nivelPrioridad": 2,
  "descripcionPrioridad": "Muy urgente",
  "fechaRegistro": "2026-03-12T10:00:00Z"
}
```

---

### 🔄 Actualizar signos vitales
**PUT** `/api/pacientes/{id}`

Al actualizar los signos vitales, el sistema recalcula automáticamente el nivel de prioridad.

```bash
curl -X PUT https://localhost:7291/api/pacientes/1 \
  -H "Content-Type: application/json" \
  -d '{
    "frecuenciaCardiaca": 75,
    "temperatura": 37.2,
    "presionSistolica": 120,
    "presionDiastolica": 80
  }'
```

**Respuesta exitosa `200 OK`:**
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "edad": 45,
  "documento": "123456789",
  "frecuenciaCardiaca": 75,
  "temperatura": 37.2,
  "presionSistolica": 120,
  "presionDiastolica": 80,
  "nivelPrioridad": 5,
  "descripcionPrioridad": "No urgente",
  "fechaRegistro": "2026-03-12T10:00:00Z"
}
```

---

### 🗑️ Eliminar paciente
**DELETE** `/api/pacientes/{id}`

```bash
curl -X DELETE https://localhost:7291/api/pacientes/1
```

**Respuesta exitosa `204 No Content`**

---

## 🔀 Repositorio en memoria (opcional)

El proyecto incluye una implementación alternativa del repositorio en memoria (`RepositorioPacienteMemoria`) que no requiere base de datos. Para usarla, cambia esta línea en `Program.cs`:

```csharp
// Con SQL Server (por defecto)
builder.Services.AddScoped<IRepositorioPaciente, RepositorioPaciente>();

// Con memoria (sin base de datos)
builder.Services.AddSingleton<IRepositorioPaciente, RepositorioPacienteMemoria>();
```

---

## 🛠️ Tecnologías utilizadas

- **ASP.NET Core 10**
- **Entity Framework Core** — acceso a datos con SQL Server
- **Mapster** — mapeo entre entidades y DTOs
- **FluentValidation** — validación de datos de entrada de los request
- **Swagger / Swashbuckle** — documentación interactiva de la API
