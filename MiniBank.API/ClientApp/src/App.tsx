import {Routes, Route} from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import DashboardPage from "./pages/DashboardPage"
import ProtectedRoute from "./components/ProtectedRoute";
// import TransferPage from "./pages/TransferPage"

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route 
        path="/dashboard" 
        element={
          <ProtectedRoute>
            <DashboardPage />
          </ProtectedRoute>
        }
      />
      {/* <Route 
      path="/transfer" 
      element={
        <ProtectedRoute>
          <TransferPage />
        </ProtectedRoute>
        } 
      /> */}
    </Routes>
  )
}

export default App;