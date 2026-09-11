import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext"
import { getAccounts } from "../functions/accountApi";
import type { Account } from "../types/account";

function DashboardPage() {
    const auth = useContext(AuthContext);
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!auth?.token) return;

    getAccounts(auth?.token)
    .then((data) => setAccounts(data))
    .catch(() => setError("Не удалось загрузить счета"));
}, [auth?.token]);

    return (
        <div>
            <h1>Dashboard</h1>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {accounts.map((account) => (
                <div key={account.accountNumber}>
                    <p>Счёт: {account.accountNumber}</p>
                    <p>Баланс: {account.balance} {account.currency}</p>
                </div>
            ))}
        </div>
    );
}

export default DashboardPage;