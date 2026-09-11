import { useContext } from "react";
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";
import type { ReactNode } from "react";

function ProtectedRoute({children} : {children: ReactNode}) {
    const auth = useContext(AuthContext);

    if (!auth?.token) {
        return <Navigate to="/login" replace />;
    }

    return <>{children}</>
}

export default ProtectedRoute;