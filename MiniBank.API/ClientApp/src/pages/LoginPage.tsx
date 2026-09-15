import { useContext, useState } from "react";
import { login as loginApi } from "../functions/authApi"
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

function LoginPage() {
  const auth = useContext(AuthContext);
  const navigate = useNavigate();

  const [identifier, setIdentifier] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  
  async function handleLogin(e: React.SyntheticEvent<HTMLFormElement>) {
    e.preventDefault();
    setError("");

    try {
    const data = await loginApi({ identifier, password });
    if (data.token) {
      auth?.login(data.token);
      navigate("/dashboard");
    }
    console.log(data);
    } catch (err) {
        setError("Не удалось войти. Проверьте username/email и пароль.");
    }
  }

  return (
    <div>
      <h1>MiniBank</h1>
      <p>Добро пожаловать в банк-симулятор</p>
      <form onSubmit={handleLogin}>
        <input
          type="text"
          value={identifier}
          onChange={(e) => setIdentifier(e.target.value)}
          placeholder="Введи username/email"
        />
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Введи password"
        />
        <button type="submit">Войти</button>
      </form>
      {error && <p style={{ color: "red" }}>{error}</p>}
    </div>
  );
}

export default LoginPage;