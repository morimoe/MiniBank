import type { LoginRequest } from "../types/auth";

export async function login(request: LoginRequest): Promise<void> {
  let response: Response;
  try {
    response = await fetch("/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      credentials: "include",
      body: JSON.stringify(request),
    });
  } catch {
    throw new Error("Сервер недоступен. Попробуйте позже.");
  }

  const data = await response.json();
  if (!data.success) throw new Error(data.errorMessage || "Не удалось войти");
}

export async function getSession(): Promise<boolean> {
  const response = await fetch("/api/auth/session", {
    credentials: "include",
  });
  const data = await response.json();
  return data.active === true;
}

export async function logout(): Promise<void> {
  await fetch("/api/auth/logout", {
    method: "POST",
    credentials: "include",
  });
}