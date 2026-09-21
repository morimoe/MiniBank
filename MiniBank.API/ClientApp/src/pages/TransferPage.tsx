import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { getAccounts } from "../functions/accountApi";
import { transferMoney } from "../functions/transactionApi";
import type { Account } from "../types/account";
import type { TransferRequest } from "../types/transfer"; 
import { useNavigate } from "react-router-dom";

function TransferPage() {
  const auth = useContext(AuthContext);
  const navigate = useNavigate();
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [fromAccountNumber, setFromAccountNumber] = useState("");
  const [toAccountNumber, setToAccountNumber] = useState("");
  const [amount, setAmount] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState(false);

  useEffect(() => {
  if (!auth?.isAuthenticated) return;

  getAccounts()
    .then((data) => setAccounts(data))
    .catch(() => setError("Не удалось загрузить счета"));
}, [auth?.isAuthenticated]);

async function handleTransfer(e: React.SyntheticEvent<HTMLFormElement>) {
  e.preventDefault();
  setError("");
  setSuccess(false);

  try {
    const request: TransferRequest = {
      fromAccountNumber,
      toAccountNumber,
      amount: Number(amount),
      description,
    };

    await transferMoney(request);
    setSuccess(true);
  } catch (err) {
    if (err instanceof Error) {
      setError(err.message);
    } else {
      setError("Не удалось выполнить перевод");
    }
  }
}

  return (
  <div className="transfer-page">
    <button className="transfer-back" onClick={() => navigate("/dashboard")}>
      ‹ Вернуться на панель управления
    </button>

    <h1 className="transfer-title">Перевод денег</h1>

    <form onSubmit={handleTransfer}>
      <div className="transfer-panel">
        <select
          className="transfer-field field-select"
          value={fromAccountNumber}
          onChange={(e) => setFromAccountNumber(e.target.value)}
        >
          <option value="">Выбрать счёт</option>
          {accounts.map((account) => (
            <option key={account.accountNumber} value={account.accountNumber}>
              {account.accountNumber} ({account.balance} {account.currency})
            </option>
          ))}
        </select>

        <input
          className="transfer-field field-amount"
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          placeholder="Сумма"
        />

        <input
          className="transfer-field field-to"
          type="text"
          value={toAccountNumber}
          onChange={(e) => setToAccountNumber(e.target.value)}
          placeholder="Введите номер аккаунта получателя"
        />

        <textarea
          className="transfer-field field-desc"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Описание (необяз.)"
        />
      </div>

      <button className="transfer-submit" type="submit">Перевести</button>
    </form>

    {error && <p className="auth-error">{error}</p>}
    {success && <p style={{ color: "#3D7A5D", textAlign: "center", fontWeight: 700 }}>Перевод выполнен успешно</p>}
  </div>
);
}

export default TransferPage;