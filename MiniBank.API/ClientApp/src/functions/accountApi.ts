import type { Account } from "../types/account";

export async function getAccounts(token: string): Promise<Account[]> {
    const response = await fetch("/api/account", {
        headers: { 
            Authorization: `Bearer ${token}`,
    },    
});

if (!response.ok) {
    throw new Error("Не удалось загрузить счета");
}

return await response.json();
}