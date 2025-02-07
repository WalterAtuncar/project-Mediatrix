import { Bell, User } from 'lucide-react';
import { useAuth } from '../../contexts/AuthContext';

const Header = () => {
  const { username } = useAuth();

  return (
    <header className="bg-[#0B2F4E] shadow-sm">
      <div className="flex justify-between items-center px-8 py-8">
        <h1 className="text-2xl font-semibold text-white">
          Entidades Gubernamentales
        </h1>
        <div className="flex items-center space-x-4">
          <button className="text-white hover:text-gray-200">
            <Bell className="w-6 h-6" />
          </button>
          <button className="flex items-center text-white hover:text-gray-200">
            <User className="w-6 h-6" />
            <span className="ml-2">{username}</span>
          </button>
        </div>
      </div>
    </header>
  );
}

export default Header;