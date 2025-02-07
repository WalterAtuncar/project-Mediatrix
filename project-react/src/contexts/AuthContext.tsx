import React, { createContext, useContext, useState } from 'react';
import { auth } from '../services/auth';

interface AuthContextType {
  username: string;
  updateUsername: (newUsername: string) => void;
}

const AuthContext = createContext<AuthContextType>({
  username: auth.getUsername(),
  updateUsername: () => {}
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [username, setUsername] = useState(auth.getUsername());

  const updateUsername = (newUsername: string) => {
    setUsername(newUsername);
  };

  return (
    <AuthContext.Provider value={{ username, updateUsername }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext); 