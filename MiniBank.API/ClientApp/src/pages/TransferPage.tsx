import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { getAccounts } from "../functions/accountApi";
import { transferMoney } from "../functions/transactionApi";
import type { Account } from "../types/account";
import type { TransferRequest } from "../types/transfer"; 
import { Link } from "react-router-dom";

function TransferPage() {
  const auth = useContext(AuthContext);
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [fromAccountNumber, setFromAccountNumber] = useState("");
  const [toAccountNumber, setToAccountNumber] = useState("");
  const [amount, setAmount] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    if (!auth?.token) return;

    getAccounts(auth?.token)
    .then((data) => setAccounts(data))
    .catch(() => setError("Не удалось загрузить счета"));
  }, [auth?.token]);

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

      await transferMoney(request, auth!.token!);
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
    <div>
      <h1>Перевод денег</h1>
      <form onSubmit={handleTransfer}>
        <select
          value={fromAccountNumber}
          onChange={(e) => setFromAccountNumber(e.target.value)}
        >
          <option value="">Выберите счёт</option>
          {accounts.map((account) => (
            <option key={account.accountNumber} value={account.accountNumber}>
              {account.accountNumber} ({account.balance} {account.currency})
            </option>
          ))}
        </select>

        <input
          type="text"
          value={toAccountNumber}
          onChange={(e) => setToAccountNumber(e.target.value)}
          placeholder="Номер счёта получателя"
        />
        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          placeholder="Сумма"
        />
        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Описание (необязательно)"
        />

        <button type="submit">Перевести</button>
      </form>

      {error && <p style={{ color: "red" }}>{error}</p>}
      {success && <p style={{ color: "green" }}>Перевод выполнен успешно</p>}

      <Link to="/dashboard">Вернуться обратно на панель управления</Link>
    </div>
  );
}

export default TransferPage;