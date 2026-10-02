# Práctica Calificada 01 – Taller Mecánico (ASP.NET Core 10 · Database First)

## Estructura (Pregunta 2)
```
UESAN.TALLER.slnx
├── Pregunta1/TallerMecanico.sql          → script de BD + datos de prueba
├── UESAN.TALLER.API/                      → Web API (presentación)
│   ├── Controllers/
│   │   ├── TipoServicioController.cs      → Pregunta 4 (DbContext directo)
│   │   └── OrdenServicioController.cs     → Pregunta 5 (Repository + Service)
│   ├── Pregunta6/                         → colección Postman (JSON)
│   └── Pregunta7/                         → respuestas de reflexión
└── UESAN.TALLER.CORE/                     → Biblioteca de clases
    ├── Core/
    │   ├── Entities/      (Scaffold)
    │   ├── DTOs/          OrdenServicioDTO.cs
    │   └── Interfaces/    IOrdenServicioRepository, IOrdenServicioService
    └── Infrastructure/
        ├── Data/          TallerDbContext (Scaffold)
        ├── Repositories/  OrdenServicioRepository
        └── Services/      OrdenServicioService (lógica de negocio)
```

## Scaffolding (Pregunta 3)
Consola del Administrador de paquetes · Proyecto predeterminado: **UESAN.TALLER.CORE** · Proyecto de inicio: **UESAN.TALLER.API**

```
Scaffold-DbContext "Server=.;Database=TallerMecanico;Integrated Security=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Core/Entities -ContextDir Infrastructure/Data -Context TallerDbContext -NoPluralize -NoOnConfiguring -Force
```

## Ejecutar
1. Cambiar `Server=.` en `UESAN.TALLER.API/appsettings.json` por el nombre del servidor SQL.
2. Ejecutar perfil **http** → `http://localhost:5166`

## Endpoints
| Método | TipoServicio | OrdenServicio |
|---|---|---|
| GET    | /api/TipoServicio      | /api/OrdenServicio      |
| GET    | /api/TipoServicio/{id} | /api/OrdenServicio/{id} |
| POST   | /api/TipoServicio      | /api/OrdenServicio      |
| PUT    | /api/TipoServicio/{id} | /api/OrdenServicio/{id} |
| DELETE | /api/TipoServicio/{id} | /api/OrdenServicio/{id} |
