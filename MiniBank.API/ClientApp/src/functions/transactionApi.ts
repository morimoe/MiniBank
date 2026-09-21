import type { Transaction } from "../types/transaction";
import type { TransferRequest, TransferResult } from "../types/transfer";

export async function getTransactionHistory(
    accountNumber: string,
): Promise<Transaction[]> {
    const response = await fetch(`/api/transaction?accountNumber=${accountNumber}`,
        {
            credentials: "include",
        }
    );

    if (!response.ok) {
        throw new Error("Не удалось загрузить историю транзакций");
    }

    return await response.json();
}

export async function transferMoney(request: TransferRequest): Promise<TransferResult> {
    const response = await fetch("/api/transaction/transfer", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify(request),
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data.errorMessage || "Не удалось провести транзакцию");
    }

    return data;
}