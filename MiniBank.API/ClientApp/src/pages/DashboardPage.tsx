import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext"
import { getAccounts } from "../functions/accountApi";
import { getCurrentUser } from "../functions/userApi";
import { getTransactionHistory } from "../functions/transactionApi";
import type { Account } from "../types/account";
import type { User } from "../types/user";
import type { Transaction } from "../types/transaction";
import Header from "../components/Header";
import { Link } from "react-router-dom";

function DashboardPage() {
    const auth = useContext(AuthContext);
    const [user, setUser] = useState<User | null>(null);
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [error, setError] = useState("");
    const [transactionsByAccount, setTransactionsByAccount] = useState<Record<string, Transaction[]>>({});

    useEffect(() => {
        if (!auth?.token) return;

    getCurrentUser(auth.token)
    .then((data) => setUser(data))
    .catch(() => setError("Не удалось загрузить пользователя"));

    getAccounts(auth?.token)
    .then((data) => setAccounts(data))
    .catch(() => setError("Не удалось загрузить счета"));
}, [auth?.token]);

    useEffect(() => {
    if (!auth?.token || accounts.length === 0) return;

    Promise.all(
        accounts.map((account) =>
        getTransactionHistory(account.accountNumber, auth.token!)
        .then((data) => ({
            accountNumber: account.accountNumber,
            transactions: data
            .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
            .slice(0, 5),
        }))
        )
    )
        .then((results) => {
        const grouped: Record<string, Transaction[]> = {};
        results.forEach((r) => {
            grouped[r.accountNumber] = r.transactions;
        });
        setTransactionsByAccount(grouped);
        })
        .catch(() => setError("Не удалось загрузить историю"));
    }, [auth?.token, accounts]);

    return (
        <div>
            <Header />
            <h1>Dashboard</h1>
            {user && <p>Привет, {user.name}</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}

            <h2>Счета</h2>
            {accounts.map((account) => (
                <div key={account.accountNumber}>
                    <p>Счёт: {account.accountNumber}</p>
                    <p>Баланс: {account.balance} {account.currency}</p>
                </div>
            ))}
            
            <Link to="/transfer">Перевести деньги</Link>

            <h2>Последние транзакции</h2>
            {accounts.map((account) => (
            <div key={account.accountNumber}>
                <h3>Счёт {account.accountNumber}</h3>
                {(transactionsByAccount[account.accountNumber] ?? []).map((t, index) => (
                <p key={index}>
                    {new Date(t.createdAt).toLocaleString()}: {t.fromAccountNumber} → {t.toAccountNumber}, {t.amount} {t.currency} — {t.status}
                </p>
                ))}
            </div>
            ))}
        </div>
    );
}

export default DashboardPage;