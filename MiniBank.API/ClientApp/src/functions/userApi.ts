import type { User } from "../types/user";

export async function getCurrentUser(token: string): Promise<User> {
    const response = await fetch("/api/user/me", {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if(!response.ok) {
        throw new Error("Не удалось загрузить пользователя");
    }

    return await response.json();
}