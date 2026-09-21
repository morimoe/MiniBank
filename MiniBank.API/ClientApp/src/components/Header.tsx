import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

function Header() {
  const auth = useContext(AuthContext);
  const navigate = useNavigate();

  async function handleLogout() {
    await auth?.logout();
    navigate("/login");
  }

  return (
    <header>
      <h1>MiniBank</h1>
      <button onClick={handleLogout}>Выйти</button>
    </header>
  );
}

export default Header;