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
    await loginApi({ identifier, password });
    auth?.login();
    navigate("/dashboard");
  } catch (err) {
    setError(err instanceof Error ? err.message : "Не удалось войти.");
  }
}
  return (
  <div className="auth-page">
    <div className="auth-hero">
      <h1>MiniBank</h1>
      <p>Добро пожаловать в банк-симулятор</p>
    </div>

    <form className="auth-form" onSubmit={handleLogin}>
      <div className="auth-panel">
        <input
          className="auth-field"
          type="text"
          value={identifier}
          onChange={(e) => setIdentifier(e.target.value)}
          placeholder="Введите имя или email"
        />
        <input
          className="auth-field"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Введите пароль"
        />
      </div>
      <button className="auth-submit" type="submit">Войти</button>
      {error && <p className="auth-error">{error}</p>}
    </form>
  </div>
);
}

export default LoginPage;