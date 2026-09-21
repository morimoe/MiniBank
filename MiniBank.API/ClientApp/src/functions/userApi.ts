import type { User } from "../types/user";

export async function getCurrentUser(): Promise<User> {
  const response = await fetch("/api/user/me", {
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Не удалось загрузить пользователя");
  }

  return await response.json();
}