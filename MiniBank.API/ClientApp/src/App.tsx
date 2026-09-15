import {Routes, Route, Navigate} from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import DashboardPage from "./pages/DashboardPage"
import ProtectedRoute from "./components/ProtectedRoute";
import AnonymousRoute from "./components/AnonymousRoute";
import TransferPage from "./pages/TransferPage"

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/login" element={<AnonymousRoute><LoginPage /></AnonymousRoute>} />
      <Route 
        path="/dashboard" 
        element={
          <ProtectedRoute>
            <DashboardPage />
          </ProtectedRoute>
        }
      />
      <Route path="/transfer" element={<ProtectedRoute><TransferPage /></ProtectedRoute>} />
    </Routes>
  )
}

export default App;