import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext"
import { getAccounts } from "../functions/accountApi";
import { getCurrentUser } from "../functions/userApi";
import { getTransactionHistory } from "../functions/transactionApi";
import type { Account } from "../types/account";
import type { User } from "../types/user";
import type { Transaction } from "../types/transaction";
import { useNavigate } from "react-router-dom";

function DashboardPage() {
    const auth = useContext(AuthContext);
    const navigate = useNavigate();

    async function handleLogout() {
        await auth?.logout();
        navigate("/login");
    }

    const [user, setUser] = useState<User | null>(null);
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [error, setError] = useState("");
    const [transactionsByAccount, setTransactionsByAccount] = useState<Record<string, Transaction[]>>({});
    const [activeAccount, setActiveAccount] = useState<string | null>(null);

    useEffect(() => {
        if (!auth?.isAuthenticated) return;

        getCurrentUser()
            .then((data) => setUser(data))
            .catch(() => setError("Не удалось загрузить пользователя"));

        getAccounts()
            .then((data) => {
                setAccounts(data);
                if (data.length > 0) setActiveAccount(data[0].accountNumber);
            })
            .catch(() => setError("Не удалось загрузить счета"));
    }, [auth?.isAuthenticated]);

    useEffect(() => {
        if (!auth?.isAuthenticated || accounts.length === 0) return;

        Promise.all(
            accounts.map((account) =>
                getTransactionHistory(account.accountNumber)
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
    }, [auth?.isAuthenticated, accounts]);

    return (
    <div className="dashboard-page">
        <div className="dashboard-topbar">
        <div className="dashboard-topbar-actions">
            <button className="dashboard-logout" onClick={handleLogout}>‹ Выйти из аккаунта</button>
            <button className="dashboard-transfer" onClick={() => navigate("/transfer")}>Совершить транзакцию ›</button>
        </div>
        {user && <span className="dashboard-greeting">Привет, {user.name}</span>}
        </div>
        <h1 className="dashboard-title">Панель управления</h1>
        {error && <p className="auth-error">{error}</p>}

        <div className="dashboard-grid">
        <div className="dashboard-panel accounts-panel">
            {accounts.map((account) => (
            <div
                key={account.accountNumber}
                className={`account-card ${activeAccount === account.accountNumber ? "is-active" : ""}`}
                onMouseEnter={() => setActiveAccount(account.accountNumber)}
            >
                <p className="account-number">Счёт: {account.accountNumber}</p>
                <p>Баланс: {account.balance} {account.currency}</p>
            </div>
            ))}
        </div>

        <div className="dashboard-panel">
            {activeAccount && (transactionsByAccount[activeAccount] ?? []).length === 0 && (
            <p className="transactions-empty">Транзакций пока нет</p>
            )}
            {activeAccount &&
            (transactionsByAccount[activeAccount] ?? []).map((t, index) => (
                <div className="transaction-card" key={index}>
                <p>{t.fromAccountNumber} → {t.toAccountNumber}</p>
                <p className="amount">{t.amount} {t.currency}</p>
                <p>{new Date(t.createdAt).toLocaleString()}, {t.status}</p>
                <p>{t.description}</p>
                </div>
            ))}
        </div>
        </div>
    </div>
    );
}

export default DashboardPage;