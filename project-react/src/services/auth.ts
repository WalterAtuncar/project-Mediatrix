// Importamos el servicio api
import { api } from './api';

export interface LoginCredentials {
  username: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

export const auth = {
  login: async (credentials: LoginCredentials) => {
    try {
      const response = await api.post<LoginResponse>('/Auth/login', credentials);
      
      if (!response.isSuccess || !response.data?.token) {
        throw new Error(response.error || 'Error al iniciar sesión');
      }

      const token = response.data.token;
      localStorage.setItem('token', token);
      localStorage.setItem('username', credentials.username);
      
      return token;
    } catch (error) {
      console.error('Error en login:', error);
      throw error;
    }
  },

  isAuthenticated: () => {
    return localStorage.getItem('token') !== null;
  },

  getUsername: () => {
    return localStorage.getItem('username') || 'Usuario';
  },

  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
  }
}; 