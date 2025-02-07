import { Link, useLocation } from 'react-router-dom';
import { Search, PlusCircle, LucideIcon } from 'lucide-react';

interface MenuItem {
  path: string;
  icon: string | LucideIcon;
  label: string;
}

const Sidebar = () => {
  const location = useLocation();

  const menuItems: MenuItem[] = [
    { path: '/', icon: '/src/assets/images/home.svg', label: 'Inicio' },
    { path: '/consulta', icon: Search, label: 'Consulta' },
    { path: '/crear-registro', icon: PlusCircle, label: 'Crear registro' },
  ];

  return (
    <aside className="w-64 bg-[#0B2F4E] shadow-lg">
      <div className="flex flex-col items-center pt-8">
        <img 
          src="/src/assets/images/sb-logo.png"
          alt="SB Logo"
          className="h-10 w-auto mb-4"
          style={{ width: '77%', height: 'auto', marginTop: '15px' }}
        />
        <nav className="px-4 mt-8 w-full">
          {menuItems.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              className={`flex items-center p-3 mb-2 rounded-lg ${
                location.pathname === item.path
                  ? 'bg-white/10 text-white'
                  : 'text-white/80 hover:bg-white/5 hover:text-white'
              }`}
            >
              {typeof item.icon === 'string' ? (
                <img src={item.icon} alt="Home Icon" className="w-5 h-5 mr-3" />
              ) : (
                <item.icon className="w-5 h-5 mr-3" />
              )}
              {item.label}
            </Link>
          ))}
        </nav>
      </div>
    </aside>
  );
}

export default Sidebar;