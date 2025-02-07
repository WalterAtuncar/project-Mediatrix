import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { auth } from '../services/auth';

export const withAuth = <P extends object>(
  WrappedComponent: React.ComponentType<P>
) => {
  return (props: P) => {
    const navigate = useNavigate();

    useEffect(() => {
      if (!auth.isAuthenticated()) {
        navigate('/', { replace: true });
      }
    }, [navigate]);

    if (!auth.isAuthenticated()) {
      return null;
    }

    return <WrappedComponent {...props} />;
  };
}; 