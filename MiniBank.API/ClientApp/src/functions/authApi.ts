import type { LoginRequest, LoginResult } from "../types/auth";

export async function login(request: LoginRequest): Promise<LoginResult> {
  let response: Response;
  try {
    response = await fetch("/api/auth", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(request),
    });
  } catch {
    throw new Error("Сервер недоступен. Попробуйте позже.");
  }

  if (!response.ok) throw new Error("Ошибка сервера. Попробуйте позже.");

  const data = await response.json();
  if (!data.success) throw new Error(data.errorMessage || "Не удалось войти");
  return data;
}