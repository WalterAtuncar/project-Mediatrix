import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/layout/Layout';
import Home from './pages/Home';
import Consulta from './pages/Consulta';
import CrearRegistro from './pages/CrearRegistro';
import { AuthProvider } from './contexts/AuthContext';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Layout>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/consulta" element={<Consulta />} />
            <Route path="/crear-registro" element={<CrearRegistro />} />
          </Routes>
        </Layout>
      </Router>
    </AuthProvider>
  );
}

export default App;