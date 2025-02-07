# Mediatrix API

## Descripción
Mediatrix API es un sistema de gestión de entidades gubernamentales que permite administrar y organizar diferentes instituciones públicas y sus categorías. El sistema implementa una arquitectura moderna y escalable siguiendo los principios de Clean Architecture y CQRS.

## Arquitectura

### Estructura de Capas 
[SB].[MediatrixApi]/
├── src/
│ ├── Api/ # Capa de presentación
│ └── Core/ # Núcleo de la aplicación
│ ├── Dominio/ # Capa de dominio
│ ├── Aplicacion/ # Capa de aplicación
│ ├── Infraestructura/ # Capa de infraestructura
│ └── Servicios/ # Servicios compartidos


### Capas del Sistema

#### 1. API (Capa de Presentación)
- Controllers para manejar las peticiones HTTP
- Configuración de la aplicación
- Middleware y filtros
- Gestión de autenticación JWT
- Manejo de respuestas API estandarizadas

#### 2. Dominio
- Entidades core del negocio
- Interfaces de repositorios
- Modelos de dominio
- Reglas de negocio

#### 3. Aplicación
- Implementación de CQRS (Commands y Queries)
- DTOs (Data Transfer Objects)
- Lógica de negocio
- Mapeo de entidades

#### 4. Infraestructura
- Implementación de repositorios
- Configuración de Entity Framework Core
- Persistencia de datos
- Migraciones

#### 5. Servicios
- Servicios compartidos
- Implementación de autenticación
- Utilidades comunes

## Patrones de Diseño Implementados

1. **CQRS (Command Query Responsibility Segregation)**
   - Separación de operaciones de lectura y escritura
   - Commands para modificaciones
   - Queries para consultas

2. **Repository Pattern**
   - Implementación genérica para todas las entidades
   - Abstracción de la capa de datos
   - Manejo consistente de operaciones CRUD

3. **Mediator Pattern (MediatR)**
   - Desacoplamiento de componentes
   - Manejo de comandos y queries

4. **Clean Architecture**
   - Separación clara de responsabilidades
   - Dependencias hacia el centro
   - Alta cohesión, bajo acoplamiento

## Características Principales

### Seguridad
- Autenticación JWT
- Autorización basada en roles
- Tokens configurables

### Gestión de Datos
- Eliminación lógica (soft delete)
- Filtros automáticos para registros eliminados
- Manejo de relaciones entre entidades

### API RESTful
- Endpoints CRUD completos
- Respuestas estandarizadas
- Manejo de errores consistente

### Características Técnicas
- NET 8.0
- Entity Framework Core
- SQLite como base de datos
- Swagger para documentación
- Logging integrado

## Entidades Principales

### CategoriaEntidad
- Gestión de categorías de entidades gubernamentales
- Propiedades básicas y relaciones
- Eliminación lógica

### EntidadGubernamental
- Gestión de instituciones gubernamentales
- Relaciones con categorías
- Información detallada de cada entidad

## Funcionalidades Destacadas

1. **Dashboard**
   - Estadísticas generales
   - Conteo de entidades y categorías
   - Información resumida

2. **Menú Jerárquico**
   - Visualización estructurada de categorías
   - Listado de entidades por categoría
   - Detalles bajo demanda

3. **Gestión de Entidades**
   - CRUD completo
   - Validaciones
   - Manejo de relaciones

## Configuración y Despliegue

### Requisitos
- .NET 8.0 SDK
- SQLite

### Configuración
1. Clonar el repositorio
2. Restaurar paquetes NuGet
3. Actualizar cadena de conexión
4. Ejecutar migraciones

### Variables de Entorno
- JWT Settings en appsettings.json
- Configuración de logging
- CORS settings

## Mejores Prácticas Implementadas

1. **Logging**
   - Registro detallado de operaciones
   - Manejo de excepciones
   - Trazabilidad

2. **Manejo de Errores**
   - Respuestas HTTP apropiadas
   - Mensajes de error descriptivos
   - Excepciones personalizadas

3. **Validaciones**
   - Validación de modelos
   - Reglas de negocio
   - Integridad de datos

4. **Código Limpio**
   - Nombres descriptivos
   - Responsabilidad única