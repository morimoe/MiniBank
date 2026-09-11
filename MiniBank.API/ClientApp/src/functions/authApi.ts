import type { LoginRequest, LoginResult } from "../types/auth";

export async function login(request: LoginRequest): Promise<LoginResult> {
    const response = await fetch("/api/auth", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(request),
    });

    const data = await response.json();

    if (!data.success) {
        throw new Error(data.errorMessage || "Не удалось войти");
    }
    
    return data;
}