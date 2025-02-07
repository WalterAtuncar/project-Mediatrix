export interface ApiResponse<T> {
  statusCode: number;
  data?: T;
  error?: string;
  isSuccess: boolean;
}

export const api = {
  baseUrl: import.meta.env.VITE_API_URL || 'https://localhost:7087/api',

  async request<T>(endpoint: string, options?: RequestInit): Promise<ApiResponse<T>> {
    const url = `${this.baseUrl}${endpoint}`;
    const token = localStorage.getItem('token');

    const headers: HeadersInit = {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(options?.headers || {})
    };

    try {
      const response = await fetch(url, {
        ...options,
        headers
      });

      const data = await response.json();
      return data as ApiResponse<T>;
    } catch (error) {
      return {
        statusCode: 500,
        error: error instanceof Error ? error.message : 'Error desconocido',
        isSuccess: false
      };
    }
  },

  get<T>(endpoint: string) {
    return this.request<T>(endpoint);
  },

  post<T>(endpoint: string, body: any) {
    return this.request<T>(endpoint, {
      method: 'POST',
      body: JSON.stringify(body)
    });
  },

  put<T>(endpoint: string, body: any) {
    return this.request<T>(endpoint, {
      method: 'PUT',
      body: JSON.stringify(body)
    });
  },

  delete<T>(endpoint: string) {
    return this.request<T>(endpoint, {
      method: 'DELETE'
    });
  }
};