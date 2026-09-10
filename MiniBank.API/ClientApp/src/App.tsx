import { useState } from "react";

function App() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  
  async function handleLogin(e: React.SyntheticEvent<HTMLFormElement>) {
    e.preventDefault();

    try {
    const response = await fetch("/api/Auth", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({email, password}),
    });
    
    const data = await response.json();
    console.log(data);
    } catch (error) {
      console.error("Ошибка входа:", error);
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
    </div>
  );
}

export default App;