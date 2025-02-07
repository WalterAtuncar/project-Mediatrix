import { auth } from './auth';
import { api, ApiResponse } from './api';

export interface CategoriaEntidad {
  categoriaId: number;
  nombre: string;
  descripcion?: string;
  estaEliminado?: boolean;
  entidadesGubernamentales?: EntidadGubernamental[];
}

export interface EntidadGubernamental {
  entidadId: number;
  nombre: string;
  siglas?: string;
  categoriaId: number;
  direccion: string;
  nombreEncargado?: string;
  fechaCreacion: Date;
  estaEliminado: boolean;
  categoria?: CategoriaEntidad;
}

export interface DashboardStats {
  totalCategorias: number;
  totalEntidades: number;
}

export const getEntidadesStats = async () => {
  const response = await api.get<DashboardStats>('/CategoriaEntidad/estadisticas');
  
  if (!response.isSuccess) {
    throw new Error(response.error || 'Error al obtener estadísticas');
  }

  return response.data!;
};

// Agregamos las nuevas interfaces
interface MenuJerarquicoResponse {
  categorias: CategoriaMenuResponse[];
}

interface CategoriaMenuResponse {
  categoriaId: number;
  nombre: string;
  descripcion: string;
  totalEntidades: number;
  entidades: EntidadGubernamentalResponse[];
}

interface EntidadGubernamentalResponse {
  entidadId: number;
  nombre: string;
  siglas: string;
  direccion: string;
  nombreEncargado: string;
  fechaCreacion: string;
  categoria: {
    categoriaId: number;
    nombre: string;
    descripcion: string;
  };
}

// Agregamos el método para obtener el menú jerárquico
export const getMenuJerarquico = async () => {
  const response = await api.get<MenuJerarquicoResponse>('/EntidadGubernamental/menu-jerarquico');
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al obtener el menú jerárquico');
  }

  return {
    categorias: response.data.categorias.map(cat => ({
      categoriaId: cat.categoriaId,
      nombre: cat.nombre,
      descripcion: cat.descripcion,
      estaEliminado: false,
      entidadesGubernamentales: cat.entidades.map(ent => ({
        ...ent,
        categoriaId: ent.categoria.categoriaId,
        estaEliminado: false,
        fechaCreacion: new Date(ent.fechaCreacion),
        categoria: {
          ...ent.categoria,
          estaEliminado: false
        }
      }))
    }))
  };
};

// Agregamos la interfaz para la respuesta
interface CategoriaResponse {
  categoriaId: number;
  nombre: string;
  descripcion: string;
  estaEliminado: boolean;
  entidadesGubernamentales: null;
}

// Agregamos el método para obtener las categorías
export const getCategorias = async () => {
  const response = await api.get<CategoriaResponse[]>('/CategoriaEntidad');
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al obtener las categorías');
  }

  // Transformamos la respuesta para que coincida con CategoriaEntidad
  return response.data.map(cat => ({
    ...cat,
    entidadesGubernamentales: undefined // Convertimos null a undefined
  }));
};

// Actualizamos la interfaz de respuesta de entidades
export interface EntidadResponse {
  entidadId: number;
  nombre: string;
  siglas: string;
  categoriaId: number;
  direccion: string;
  nombreEncargado: string;
  fechaCreacion: string;  // Cambiado a string ya que viene así del API
  estaEliminado: boolean;
  categoria: {
    categoriaId: number;
    nombre: string;
    descripcion: string;
  };
}

// Actualizamos el método para obtener las entidades
export const getEntidades = async (): Promise<EntidadResponse[]> => {
  const response = await api.get<ApiResponse<EntidadResponse[]>>('/EntidadGubernamental');
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al obtener las entidades');
  }

  // Asegurarnos de que response.data es un array
  const entidades = Array.isArray(response.data) ? response.data : [];
  
  return entidades;
};

// Agregamos la interfaz para el request de crear categoría
interface CreateCategoriaRequest {
  nombre: string;
  descripcion: string;
}

// Agregamos la interfaz para el request de actualizar categoría
interface UpdateCategoriaRequest {
  categoriaId: number;
  nombre: string;
  descripcion: string;
}

// Agregamos el método para crear categoría
export const createCategoria = async (categoria: CreateCategoriaRequest): Promise<CategoriaResponse> => {
  const response = await api.post<CategoriaResponse>('/CategoriaEntidad', categoria);
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al crear la categoría');
  }

  return response.data;
};

export const updateCategoria = async (categoria: UpdateCategoriaRequest): Promise<CategoriaResponse> => {
  const response = await api.put<CategoriaResponse>(`/CategoriaEntidad/${categoria.categoriaId}`, categoria);
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al actualizar la categoría');
  }

  return response.data;
};

export const deleteCategoria = async (categoriaId: number): Promise<void> => {
  const response = await api.delete<void>(`/CategoriaEntidad/${categoriaId}`);
  
  if (!response.isSuccess) {
    throw new Error(response.error || 'Error al eliminar la categoría');
  }
};

interface CreateEntidadRequest {
  nombre: string;
  siglas: string;
  categoriaId: number;
  direccion: string;
  nombreEncargado: string;
}

interface UpdateEntidadRequest {
  entidadId: number;
  nombre: string;
  categoriaId: number;
  siglas: string;
  direccion: string;
  nombreEncargado: string;
}

export const createEntidad = async (entidad: CreateEntidadRequest): Promise<EntidadResponse> => {
  const response = await api.post<EntidadResponse>('/EntidadGubernamental', entidad);
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al crear la entidad');
  }

  return response.data;
};

export const updateEntidad = async (entidad: UpdateEntidadRequest): Promise<EntidadResponse> => {
  const response = await api.put<EntidadResponse>(`/EntidadGubernamental/${entidad.entidadId}`, entidad);
  
  if (!response.isSuccess || !response.data) {
    throw new Error(response.error || 'Error al actualizar la entidad');
  }

  return response.data;
};

export const deleteEntidad = async (entidadId: number): Promise<void> => {
  const response = await api.delete<void>(`/EntidadGubernamental/${entidadId}`);
  
  if (!response.isSuccess) {
    throw new Error(response.error || 'Error al eliminar la entidad');
  }
}; 