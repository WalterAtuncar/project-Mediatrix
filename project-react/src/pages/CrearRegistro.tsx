import { useState, useEffect } from 'react';
import { 
  Building2, Landmark, Plus, Search, Edit2, Trash2, 
  X, Save
} from 'lucide-react';
import { CategoriaEntidad, EntidadGubernamental, EntidadResponse, getCategorias, getEntidades, createCategoria, updateCategoria, deleteCategoria, createEntidad, updateEntidad, deleteEntidad } from '../services/entidades';
import { withAuth } from '../components/withAuth';
import { showAlert } from '../services/alert';

type FormMode = 'list' | 'create' | 'edit';
type EntityType = 'categoria' | 'entidad';

interface FormData {
  categoria: Partial<CategoriaEntidad>;
  entidad: Partial<EntidadGubernamental>;
}

const CrearRegistro = () => {
  const [activeTab, setActiveTab] = useState<EntityType>('categoria');
  const [mode, setMode] = useState<FormMode>('list');
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedItem, setSelectedItem] = useState<CategoriaEntidad | EntidadResponse | null>(null);
  const [formData, setFormData] = useState<FormData>({
    categoria: {},
    entidad: {}
  });
  const [categorias, setCategorias] = useState<CategoriaEntidad[]>([]);
  const [entidades, setEntidades] = useState<EntidadResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        if (activeTab === 'categoria') {
          const data = await getCategorias();
          setCategorias(data);
        } else {
          const data = await getEntidades();
          setEntidades(data);
        }
      } catch (err) {
        console.error('Error al cargar datos:', err);
        setError('Error al cargar los datos');
      } finally {
        setLoading(false);
      }
    };

    setLoading(true);
    fetchData();
  }, [activeTab]);

  // Función para manejar la creación/edición de categorías
  const handleCategoriaSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (mode === 'create') {
        await createCategoria({
          nombre: formData.categoria.nombre || '',
          descripcion: formData.categoria.descripcion || ''
        });
        showAlert('success', 'Categoría creada exitosamente');
      } else {
        await updateCategoria({
          categoriaId: formData.categoria.categoriaId!,
          nombre: formData.categoria.nombre || '',
          descripcion: formData.categoria.descripcion || ''
        });
        showAlert('success', 'Categoría actualizada exitosamente');
      }
      
      // Recargar las categorías
      const data = await getCategorias();
      setCategorias(data);
      setMode('list');
    } catch (err) {
      console.error('Error al guardar la categoría:', err);
      showAlert('error', 'Error al guardar la categoría');
    }
  };

  // Función para manejar la creación/edición de entidades
  const handleEntidadSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (mode === 'create') {
        await createEntidad({
          nombre: formData.entidad.nombre || '',
          siglas: formData.entidad.siglas || '',
          categoriaId: formData.entidad.categoriaId || 0,
          direccion: formData.entidad.direccion || '',
          nombreEncargado: formData.entidad.nombreEncargado || ''
        });
        showAlert('success', 'Entidad creada exitosamente');
      } else {
        await updateEntidad({
          entidadId: formData.entidad.entidadId!,
          nombre: formData.entidad.nombre || '',
          categoriaId: formData.entidad.categoriaId || 0,
          siglas: formData.entidad.siglas || '',
          direccion: formData.entidad.direccion || '',
          nombreEncargado: formData.entidad.nombreEncargado || ''
        });
        showAlert('success', 'Entidad actualizada exitosamente');
      }
      
      // Recargar las entidades
      const data = await getEntidades();
      setEntidades(data);
      setMode('list');
    } catch (err) {
      console.error('Error al guardar la entidad:', err);
      showAlert('error', 'Error al guardar la entidad');
    }
  };

  // Agregamos la función para manejar la eliminación
  const handleDeleteCategoria = async (categoria: CategoriaEntidad) => {
    try {
      const result = await showAlert('warning', '¿Está seguro de eliminar esta categoría?', {
        title: 'Confirmar eliminación',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
        icon: 'warning',
        toast: false,
        position: 'center'
      });

      if (result.isConfirmed) {
        await deleteCategoria(categoria.categoriaId);
        showAlert('success', 'Categoría eliminada exitosamente');
        
        // Recargar las categorías
        const data = await getCategorias();
        setCategorias(data);
      }
    } catch (err) {
      console.error('Error al eliminar la categoría:', err);
      showAlert('error', 'Error al eliminar la categoría');
    }
  };

  // Agregamos la función para manejar la eliminación de entidades
  const handleDeleteEntidad = async (entidad: EntidadResponse) => {
    try {
      const result = await showAlert('warning', '¿Está seguro de eliminar esta entidad?', {
        title: 'Confirmar eliminación',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
        icon: 'warning',
        toast: false,
        position: 'center'
      });

      if (result.isConfirmed) {
        await deleteEntidad(entidad.entidadId);
        showAlert('success', 'Entidad eliminada exitosamente');
        
        // Recargar las entidades
        const data = await getEntidades();
        setEntidades(data);
      }
    } catch (err) {
      console.error('Error al eliminar la entidad:', err);
      showAlert('error', 'Error al eliminar la entidad');
    }
  };

  const renderHeader = () => (
    <div className="flex justify-between items-center mb-6">
      <div className="flex space-x-4">
        <button
          onClick={() => setActiveTab('categoria')}
          className={`flex items-center px-4 py-2 rounded-lg ${
            activeTab === 'categoria'
              ? 'bg-[#0B2F4E] text-white'
              : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
          }`}
        >
          <Building2 className="w-5 h-5 mr-2" />
          Categorías
        </button>
        <button
          onClick={() => setActiveTab('entidad')}
          className={`flex items-center px-4 py-2 rounded-lg ${
            activeTab === 'entidad'
              ? 'bg-[#0B2F4E] text-white'
              : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
          }`}
        >
          <Landmark className="w-5 h-5 mr-2" />
          Entidades
        </button>
      </div>
      
      {mode === 'list' && (
        <button
          onClick={() => {
            setMode('create');
            setSelectedItem(null);
            setFormData({ categoria: {}, entidad: {} });
          }}
          className="flex items-center px-4 py-2 bg-[#0B2F4E] text-white rounded-lg hover:bg-[#0B2F4E]/90"
        >
          <Plus className="w-5 h-5 mr-2" />
          {activeTab === 'categoria' ? 'Nueva Categoría' : 'Nueva Entidad'}
        </button>
      )}
    </div>
  );

  const renderSearchBar = () => (
    <div className="mb-6">
      <div className="relative">
        <input
          type="text"
          placeholder="Buscar..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="w-full pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
        />
        <Search className="absolute left-3 top-2.5 h-5 w-5 text-gray-400" />
      </div>
    </div>
  );

  const renderCategoriaForm = () => (
    <form onSubmit={handleCategoriaSubmit} className="bg-white rounded-lg shadow-sm p-6">
      <h3 className="text-xl font-bold mb-6">
        {mode === 'create' ? 'Nueva Categoría' : 'Editar Categoría'}
      </h3>
      
      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Nombre
          </label>
          <input
            type="text"
            value={formData.categoria.nombre || ''}
            onChange={(e) => setFormData(prev => ({
              ...prev,
              categoria: { ...prev.categoria, nombre: e.target.value }
            }))}
            className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
            required
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Descripción
          </label>
          <textarea
            value={formData.categoria.descripcion || ''}
            onChange={(e) => setFormData(prev => ({
              ...prev,
              categoria: { ...prev.categoria, descripcion: e.target.value }
            }))}
            className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
            rows={3}
          />
        </div>
      </div>

      <div className="flex justify-end space-x-4 mt-6">
        <button
          type="button"
          onClick={() => setMode('list')}
          className="px-4 py-2 text-gray-600 hover:bg-gray-100 rounded-lg"
        >
          Cancelar
        </button>
        <button
          type="submit"
          className="px-4 py-2 bg-[#0B2F4E] text-white rounded-lg hover:bg-[#0B2F4E]/90"
        >
          <Save className="w-5 h-5 mr-2 inline-block" />
          Guardar
        </button>
      </div>
    </form>
  );

  const renderEntidadForm = () => (
    <form onSubmit={handleEntidadSubmit} className="bg-white rounded-lg shadow-sm p-6">
      <h3 className="text-xl font-bold mb-6">
        {mode === 'create' ? 'Nueva Entidad' : 'Editar Entidad'}
      </h3>
      
      <div className="grid grid-cols-2 gap-6">
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Nombre
            </label>
            <input
              type="text"
              value={formData.entidad.nombre || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, nombre: e.target.value }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Siglas
            </label>
            <input
              type="text"
              value={formData.entidad.siglas || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, siglas: e.target.value }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Categoría
            </label>
            <select
              value={formData.entidad.categoriaId || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, categoriaId: Number(e.target.value) }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
              required
            >
              <option value="">Seleccione una categoría</option>
              {categorias.map(categoria => (
                <option key={categoria.categoriaId} value={categoria.categoriaId}>{categoria.nombre}</option>
              ))}
            </select>
          </div>
        </div>

        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Dirección
            </label>
            <input
              type="text"
              value={formData.entidad.direccion || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, direccion: e.target.value }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Nombre del Encargado
            </label>
            <input
              type="text"
              value={formData.entidad.nombreEncargado || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, nombreEncargado: e.target.value }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Fecha de Creación
            </label>
            <input
              type="date"
              value={formData.entidad.fechaCreacion?.toISOString().split('T')[0] || ''}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                entidad: { ...prev.entidad, fechaCreacion: new Date(e.target.value) }
              }))}
              className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-[#0B2F4E]/20"
              required
            />
          </div>
        </div>
      </div>

      <div className="flex justify-end space-x-4 mt-6">
        <button
          type="button"
          onClick={() => setMode('list')}
          className="px-4 py-2 text-gray-600 hover:bg-gray-100 rounded-lg"
        >
          Cancelar
        </button>
        <button
          type="submit"
          className="px-4 py-2 bg-[#0B2F4E] text-white rounded-lg hover:bg-[#0B2F4E]/90"
        >
          <Save className="w-5 h-5 mr-2 inline-block" />
          Guardar
        </button>
      </div>
    </form>
  );

  const renderList = () => {
    if (activeTab === 'categoria') {
      const filteredCategorias = categorias.filter(categoria => 
        categoria.nombre.toLowerCase().includes(searchTerm.toLowerCase().trim())
      );
      return (
        <div className="bg-white rounded-lg shadow-sm overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-[#EDF0F7]">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Nombre
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Descripción
                  </th>
                  <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Acciones
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredCategorias.map((categoria) => (
                  <tr key={categoria.categoriaId}>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">{categoria.nombre}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-500">{categoria.descripcion}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                      <button
                        onClick={() => {
                          setSelectedItem(categoria);
                          setFormData(prev => ({
                            ...prev,
                            categoria: { ...categoria }
                          }));
                          setMode('edit');
                        }}
                        className="text-[#0D3048E6] hover:text-[#0D3048E6]/80 mr-4"
                      >
                        <Edit2 className="w-4 h-4" />
                      </button>
                      <button
                        onClick={() => handleDeleteCategoria(categoria)}
                        className="text-red-600 hover:text-red-800"
                      >
                        <Trash2 className="w-4 h-4" />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      );
    } else {
      const filteredEntidades = entidades.filter(entidad => 
        entidad.nombre.toLowerCase().includes(searchTerm.toLowerCase().trim())
      );
      return (
        <div className="bg-white rounded-lg shadow-sm overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-[#EDF0F7]">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Nombre
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Siglas
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Categoría
                  </th>
                  <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Acciones
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredEntidades.map((entidad) => (
                  <tr key={entidad.entidadId}>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">{entidad.nombre}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-500">{entidad.siglas}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-500">{entidad.categoria.nombre}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                      <button
                        onClick={() => {
                          setSelectedItem(entidad);
                          setFormData(prev => ({
                            ...prev,
                            entidad: { 
                              ...entidad,
                              fechaCreacion: new Date(entidad.fechaCreacion)
                            }
                          }));
                          setMode('edit');
                        }}
                        className="text-[#0D3048E6] hover:text-[#0D3048E6]/80 mr-4"
                      >
                        <Edit2 className="w-4 h-4" />
                      </button>
                      <button
                        onClick={() => handleDeleteEntidad(entidad)}
                        className="text-red-600 hover:text-red-800"
                      >
                        <Trash2 className="w-4 h-4" />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      );
    }
  };

  if (loading) {
    return <div className="flex justify-center items-center h-full">Cargando...</div>;
  }

  if (error) {
    return <div className="text-red-500 text-center">{error}</div>;
  }

  return (
    <div className="space-y-6">
      {renderHeader()}
      {mode === 'list' && (
        <>
          {renderSearchBar()}
          {renderList()}
        </>
      )}
      {mode === 'create' && activeTab === 'categoria' && renderCategoriaForm()}
      {mode === 'edit' && activeTab === 'categoria' && renderCategoriaForm()}
      {(mode === 'create' || mode === 'edit') && activeTab === 'entidad' && renderEntidadForm()}
    </div>
  );
};

export default withAuth(CrearRegistro);