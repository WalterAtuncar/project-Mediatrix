import { useState, useEffect } from 'react';
import { ChevronDown, ChevronRight, Building2, Landmark, Search } from 'lucide-react';
import { CategoriaEntidad, EntidadGubernamental, getMenuJerarquico } from '../services/entidades';
import { withAuth } from '../components/withAuth';

const Consulta = () => {
  const [expandedCategories, setExpandedCategories] = useState<number[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedEntidad, setSelectedEntidad] = useState<EntidadGubernamental | null>(null);
  const [data, setData] = useState<{ categorias: CategoriaEntidad[] }>({ categorias: [] });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const menuData = await getMenuJerarquico();
        setData(menuData);
      } catch (err) {
        console.error('Error al cargar el menú:', err);
        setError('Error al cargar los datos. Por favor, intente nuevamente.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const toggleCategory = (categoriaId: number) => {
    setExpandedCategories(prev => 
      prev.includes(categoriaId) 
        ? prev.filter(id => id !== categoriaId)
        : [...prev, categoriaId]
    );
  };

  const filteredCategorias = data.categorias
    .filter(cat => {
      if (cat.estaEliminado) return false;

      if (searchTerm.trim()) {
        const searchTermLower = searchTerm.toLowerCase().trim();
        
        const matchesCategoria = cat.nombre.toLowerCase().includes(searchTermLower);
        
        const matchesEntidades = cat.entidadesGubernamentales?.some(
          entidad => entidad.nombre.toLowerCase().includes(searchTermLower) ||
                    entidad.siglas?.toLowerCase().includes(searchTermLower)
        );

        return matchesCategoria || matchesEntidades;
      }

      return true;
    });

  if (loading) {
    return <div className="flex justify-center items-center h-full">Cargando...</div>;
  }

  if (error) {
    return <div className="text-red-500 text-center">{error}</div>;
  }

  return (
    <div className="flex h-full gap-6">
      {/* Panel izquierdo - Árbol jerárquico */}
      <div className="w-1/3 bg-white rounded-lg shadow-sm p-4">
        <div className="mb-4">
          <div className="relative">
            <input
              type="text"
              placeholder="Buscar categorías o entidades..."
              className="w-full pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
            <Search className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
          </div>
        </div>

        <div className="overflow-y-auto max-h-[calc(100vh-250px)]">
          {filteredCategorias.map(categoria => {
            const entidades = categoria.entidadesGubernamentales || [];
            const isExpanded = expandedCategories.includes(categoria.categoriaId);

            return (
              <div key={categoria.categoriaId} className="mb-2">
                <button
                  onClick={() => toggleCategory(categoria.categoriaId)}
                  className="w-full flex items-center p-2 hover:bg-gray-50 rounded-lg group"
                >
                  {isExpanded ? (
                    <ChevronDown className="h-5 w-5 text-gray-400 mr-2" />
                  ) : (
                    <ChevronRight className="h-5 w-5 text-gray-400 mr-2" />
                  )}
                  <Building2 className="h-5 w-5 text-[#0B2F4E] mr-2" />
                  <span className="text-gray-700 font-medium">{categoria.nombre}</span>
                  <span className="ml-auto text-sm text-gray-400 group-hover:text-gray-600">
                    {entidades.length}
                  </span>
                </button>

                {isExpanded && (
                  <div className="ml-9 space-y-1 mt-1">
                    {entidades.map(entidad => (
                      <button
                        key={entidad.entidadId}
                        onClick={() => setSelectedEntidad(entidad)}
                        className={`w-full flex items-center p-2 rounded-lg hover:bg-gray-50 ${
                          selectedEntidad?.entidadId === entidad.entidadId
                            ? 'bg-[#0B2F4E]/5'
                            : ''
                        }`}
                      >
                        <Landmark className="h-4 w-4 text-gray-400 mr-2" />
                        <span className="text-sm text-gray-600">{entidad.nombre}</span>
                        {entidad.siglas && (
                          <span className="ml-2 text-xs text-gray-400">({entidad.siglas})</span>
                        )}
                      </button>
                    ))}
                  </div>
                )}
              </div>
            );
          })}
        </div>
      </div>

      {/* Panel derecho - Detalles de la entidad */}
      <div className="flex-1 bg-white rounded-lg shadow-sm p-6">
        {selectedEntidad ? (
          <div>
            <div className="border-b pb-4 mb-6">
              <h3 className="text-2xl font-bold text-gray-800">{selectedEntidad.nombre}</h3>
              {selectedEntidad.siglas && (
                <p className="text-gray-500 mt-1">{selectedEntidad.siglas}</p>
              )}
            </div>

            <div className="grid grid-cols-2 gap-6">
              <div>
                <h4 className="text-sm font-semibold text-gray-600 mb-2">Información General</h4>
                <div className="space-y-4">
                  <div>
                    <p className="text-sm text-gray-500">Dirección</p>
                    <p className="text-gray-700">{selectedEntidad.direccion}</p>
                  </div>
                  {selectedEntidad.nombreEncargado && (
                    <div>
                      <p className="text-sm text-gray-500">Encargado</p>
                      <p className="text-gray-700">{selectedEntidad.nombreEncargado}</p>
                    </div>
                  )}
                </div>
              </div>
              
              <div>
                <h4 className="text-sm font-semibold text-gray-600 mb-2">Categoría</h4>
                <div className="bg-gray-50 p-4 rounded-lg">
                  <p className="text-gray-700">
                    {data.categorias.find(c => c.categoriaId === selectedEntidad.categoriaId)?.nombre}
                  </p>
                  <p className="text-sm text-gray-500 mt-1">
                    {data.categorias.find(c => c.categoriaId === selectedEntidad.categoriaId)?.descripcion}
                  </p>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <div className="h-full flex items-center justify-center text-gray-400">
            <p>Seleccione una entidad para ver sus detalles</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default withAuth(Consulta);