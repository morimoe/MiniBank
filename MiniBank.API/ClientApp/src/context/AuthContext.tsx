import { createContext, useState, useEffect, type ReactNode } from "react";
import { getSession, logout as logoutApi } from "../functions/authApi";

interface AuthContextType {
  isAuthenticated: boolean;
  loading: boolean;
  login: () => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextType | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getSession()
      .then((active) => setIsAuthenticated(active))
      .catch(() => setIsAuthenticated(false))
      .finally(() => setLoading(false));
  }, []);

  function login() {
    setIsAuthenticated(true);
  }

  async function logout() {
    await logoutApi();
    setIsAuthenticated(false);
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}