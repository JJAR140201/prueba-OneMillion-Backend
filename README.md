# RealEstate API

Una API RESTful para gestión de propiedades inmobiliarias desarrollada con ASP.NET Core 8.0 y MongoDB.

## 🏗️ Arquitectura

El proyecto sigue los principios de **Clean Architecture** con las siguientes capas:

```
├── RealEstate.Api/          # Capa de presentación (Controllers, Program.cs)
├── RealEstate.Application/  # Lógica de aplicación (Services, DTOs, Queries)
├── RealEstate.Domain/       # Entidades de dominio y abstracciones
└── RealEstate.Infrastructure/ # Implementaciones (MongoDB, Repositories)
```

## 🚀 Características

- ✅ **API RESTful** con ASP.NET Core 8.0
- ✅ **Base de datos MongoDB** con mapeo de ObjectId
- ✅ **Clean Architecture** - Separación de responsabilidades
- ✅ **CORS habilitado** para aplicaciones frontend
- ✅ **Swagger/OpenAPI** para documentación
- ✅ **Búsqueda con filtros** (nombre, dirección, rango de precios)
- ✅ **Paginación** de resultados

## 📋 Endpoints

### GET /api/Properties
Obtiene propiedades con filtros opcionales.

**Parámetros de consulta:**
- `name` (string, opcional) - Filtro por nombre
- `address` (string, opcional) - Filtro por dirección
- `minPrice` (decimal, opcional) - Precio mínimo
- `maxPrice` (decimal, opcional) - Precio máximo
- `page` (int, default: 1) - Número de página
- `pageSize` (int, default: 20) - Elementos por página

**Ejemplo de respuesta:**
```json
{
  "items": [
    {
      "id": "64f5c2b8e1234567890abcde",
      "idOwner": "64f5c2b8e1234567890abcdf",
      "name": "Sunny Loft",
      "address": "Calle 10 #20-30, Medellín",
      "price": 650000000,
      "imageUrl": ""
    }
  ],
  "total": 1,
  "page": 1,
  "pageSize": 20
}
```

### GET /api/Diagnostic/database-info
Endpoint de diagnóstico para verificar la conexión con MongoDB.

## 🛠️ Tecnologías

- **ASP.NET Core 8.0**
- **MongoDB Driver 3.5.0**
- **Swagger/OpenAPI**
- **C# 12 (.NET 8)**

## ⚙️ Configuración

### Prerrequisitos
- .NET 8.0 SDK
- MongoDB (local o remoto)

### Configuración de la base de datos
Actualiza `appsettings.json`:

```json
{
  "Mongo": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "realestate_db",
    "PropertiesCollection": "properties"
  }
}
```

### Estructura de datos en MongoDB
```javascript
// Colección: properties
{
  _id: ObjectId("..."),
  Name: "Sunny Loft",
  Address: "Calle 10 #20-30, Medellín",
  Price: NumberDecimal("650000000"),
  CodeInternal: "A001",
  Year: 2018,
  IdOwner: ObjectId("...")
}
```

## 🚀 Instalación y Ejecución

1. **Clona el repositorio:**
```bash
git clone https://github.com/tu-usuario/realestate-api.git
cd realestate-api
```

2. **Restaura las dependencias:**
```bash
dotnet restore
```

3. **Compila el proyecto:**
```bash
dotnet build
```

4. **Ejecuta la aplicación:**
```bash
cd RealEstate.Api
dotnet run
```

5. **Accede a la documentación:**
- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger

## 🧪 Ejemplos de uso

### Obtener todas las propiedades
```bash
curl "http://localhost:5000/api/Properties"
```

### Buscar por nombre
```bash
curl "http://localhost:5000/api/Properties?name=Sunny"
```

### Buscar por dirección
```bash
curl "http://localhost:5000/api/Properties?address=Medellín"
```

### Buscar por rango de precio
```bash
curl "http://localhost:5000/api/Properties?minPrice=600000000&maxPrice=700000000"
```

### Búsqueda combinada con paginación
```bash
curl "http://localhost:5000/api/Properties?name=Sunny&address=Medellín&page=1&pageSize=10"
```

## 🔧 Desarrollo

### Estructura del proyecto
```
RealEstate/
├── RealEstate.Api/
│   ├── Controllers/
│   │   ├── PropertiesController.cs
│   │   └── DiagnosticController.cs
│   ├── DI/
│   │   └── ApiServiceCollectionExtensions.cs
│   └── Program.cs
├── RealEstate.Application/
│   ├── DTOs/
│   ├── Services/
│   ├── Queries/
│   └── Mapping/
├── RealEstate.Domain/
│   ├── Entities/
│   ├── Abstractions/
│   └── Queries/
└── RealEstate.Infrastructure/
    ├── Mongo/
    └── DI/
```

### Agregar nuevos filtros
1. Actualiza `PropertySearchQuery` en Application
2. Actualiza `PropertySearchCriteria` en Domain
3. Actualiza la implementación del repositorio
4. Actualiza el mapping entre Query y Criteria

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📞 Contacto

Tu Nombre - [@tu_twitter](https://twitter.com/tu_twitter) - tu.email@ejemplo.com

Enlace del Proyecto: [https://github.com/tu-usuario/realestate-api](https://github.com/tu-usuario/realestate-api)