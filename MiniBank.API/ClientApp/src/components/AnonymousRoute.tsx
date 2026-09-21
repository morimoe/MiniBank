import { useContext } from "react";
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";
import type { ReactNode } from "react";

function AnonymousRoute({ children }: { children: ReactNode }) {
    const auth = useContext(AuthContext);

    if (auth?.loading) return null;

    if (auth?.isAuthenticated) {
        return <Navigate to="/dashboard" replace />;
    }

    return <>{children}</>;
}

export default AnonymousRoute;