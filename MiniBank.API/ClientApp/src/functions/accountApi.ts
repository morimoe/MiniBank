import type { Account } from "../types/account";

export async function getAccounts(): Promise<Account[]> {
  const response = await fetch("/api/account", {
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Не удалось загрузить счета");
  }

  return await response.json();
}