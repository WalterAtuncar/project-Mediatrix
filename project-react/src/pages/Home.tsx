import React, { useEffect, useState } from 'react';
import { Building2, Landmark } from 'lucide-react';
import { getEntidadesStats } from '../services/entidades';
import { auth } from '../services/auth';
import LoginDialog from '../components/LoginDialog';

const StatCard = ({ title, value, icon: Icon }: { title: string; value: number; icon: React.ElementType }) => (
  <div className="bg-white rounded-lg shadow p-6 flex items-center space-x-4">
    <div className="p-3 bg-blue-100 rounded-full">
      <Icon className="w-8 h-8 text-blue-600" />
    </div>
    <div>
      <h3 className="text-lg text-gray-600">{title}</h3>
      <p className="text-3xl font-bold text-gray-900">{value}</p>
    </div>
  </div>
);

const Home = () => {
  const [stats, setStats] = useState({ totalCategorias: 0, totalEntidades: 0 });
  const [showLoginDialog, setShowLoginDialog] = useState(!auth.isAuthenticated());

  useEffect(() => {
    if (auth.isAuthenticated()) {
      getEntidadesStats()
        .then(setStats)
        .catch(error => {
          console.error('Error al cargar estadísticas:', error);
        });
    }
  }, []);

  const handleLogin = async (username: string, password: string) => {
    try {
      await auth.login({ username, password });
      setShowLoginDialog(false);
      const dashboardStats = await getEntidadesStats();
      setStats(dashboardStats);
    } catch (err) {
      console.error('Error en login:', err);
      throw err;
    }
  };

  // Si no hay token, mostrar el diálogo de login
  if (showLoginDialog) {
    return <LoginDialog onLogin={handleLogin} />;
  }

  // Si hay token, mostrar el dashboard
  return (
    <>
      <div>
        <h2 className="text-2xl font-bold mb-6">Dashboard</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <StatCard
            title="Total Categorías"
            value={stats.totalCategorias}
            icon={Building2}
          />
          <StatCard
            title="Total Entidades Gubernamentales"
            value={stats.totalEntidades}
            icon={Landmark}
          />
        </div>
      </div>
    </>
  );
};

export default Home;