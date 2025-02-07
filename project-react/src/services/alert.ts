import Swal from 'sweetalert2';

type AlertType = 'success' | 'error' | 'warning' | 'info' | 'question';

interface AlertOptions {
  title?: string;
  timer?: number;
  showConfirmButton?: boolean;
  showCancelButton?: boolean;
  confirmButtonText?: string;
  cancelButtonText?: string;
  icon?: AlertType;
  toast?: boolean;
  position?: 'top' | 'top-start' | 'top-end' | 'center' | 'center-start' | 'center-end' | 'bottom' | 'bottom-start' | 'bottom-end';
}

export const showAlert = (type: AlertType, message: string, options?: AlertOptions) => {
  return Swal.fire({
    icon: type,
    title: options?.title || '',
    text: message,
    timer: options?.timer || 3000,
    showConfirmButton: options?.showConfirmButton ?? false,
    timerProgressBar: true,
    toast: true,
    position: 'top-end',
    ...options
  });
}; 