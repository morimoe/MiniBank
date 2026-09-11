import { useContext, useState } from "react";
import { login as loginApi } from "../functions/authApi"
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

function LoginPage() {
  const auth = useContext(AuthContext);
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  
  async function handleLogin(e: React.SyntheticEvent<HTMLFormElement>) {
    e.preventDefault();
    setError("");

    try {
    const data = await loginApi({ email, password });
    if (data.token) {
      auth?.login(data.token);
      navigate("/dashboard");
    }
    console.log(data);
    } catch (err) {
        setError("Не удалось войти. Проверьте email и пароль.");
    }
  }

  return (
    <div>
      <h1>MiniBank</h1>
      <p>Добро пожаловать в банк-симулятор</p>
      <form onSubmit={handleLogin}>
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Введи email"
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