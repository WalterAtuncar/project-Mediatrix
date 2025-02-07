# Sistema de Gestión de Entidades Gubernamentales

## Descripción
Sistema web para la gestión y consulta de entidades gubernamentales y sus categorías. Permite administrar la estructura jerárquica de las instituciones gubernamentales, manteniendo un registro organizado y accesible.

## Características Principales

### 1. Dashboard
- Visualización de estadísticas generales
- Contador de categorías totales
- Contador de entidades gubernamentales

### 2. Gestión de Categorías
- Crear nuevas categorías
- Editar categorías existentes
- Eliminar categorías
- Visualización en lista con búsqueda

### 3. Gestión de Entidades
- Crear nuevas entidades gubernamentales
- Editar información de entidades
- Eliminar entidades
- Asignación a categorías
- Búsqueda y filtrado

### 4. Consulta Jerárquica
- Visualización jerárquica de entidades por categoría
- Detalles expandibles
- Búsqueda integrada
- Vista detallada de información de entidades

### 5. Seguridad
- Sistema de autenticación
- Protección de rutas
- Manejo de sesiones
- Token JWT

## Tecnologías Utilizadas

### Frontend
- React 18
- TypeScript
- Tailwind CSS
- Lucide Icons
- SweetAlert2
- React Router DOM

### Arquitectura Frontend
- Componentes funcionales
- Hooks personalizados
- Context API para estado global
- Servicios modularizados
- Sistema de tipos TypeScript

### Estructura del Proyecto 
src/
├── components/ # Componentes reutilizables
├── contexts/ # Contextos de React (Auth)
├── pages/ # Componentes de página
├── services/ # Servicios y API
└── types/ # Definiciones de tipos


## Servicios API

### Endpoints Principales
- `/Auth/login` - Autenticación
- `/CategoriaEntidad` - CRUD de categorías
- `/EntidadGubernamental` - CRUD de entidades
- `/CategoriaEntidad/estadisticas` - Estadísticas
- `/EntidadGubernamental/menu-jerarquico` - Estructura jerárquica

## Configuración del Proyecto

### Variables de Entorno
env
VITE_API_URL=https://localhost:7087/api

### Instalación
bash
npm install
npm run dev

### Construcción
bash
npm run build


## Características Técnicas Destacadas

1. **Tipado Estricto**
   - Interfaces TypeScript para todos los modelos
   - Tipado de respuestas API
   - Validación de tipos en tiempo de compilación

2. **Manejo de Estado**
   - Estados locales con useState
   - Estado global con Context API
   - Gestión de efectos secundarios con useEffect

3. **Seguridad**
   - Interceptores de API
   - Manejo de tokens JWT
   - HOC de autenticación (withAuth)

4. **UI/UX**
   - Diseño responsive
   - Feedback visual con alertas
   - Indicadores de carga
   - Manejo de errores amigable

5. **Optimizaciones**
   - Lazy loading de componentes
   - Memoización cuando es necesario
   - Validaciones de formularios
   - Manejo de caché de datos

## Requisitos del Sistema
- Node.js 16+
- npm 7+
- Navegador moderno con soporte ES6

## Licencia
MIT