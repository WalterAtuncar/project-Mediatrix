namespace _SB_._MediatrixApi_._Dominio_.Entidades
{
    public class CategoriaEntidad
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaEliminado { get; set; }

        // Propiedad de navegación
        public virtual ICollection<EntidadGubernamental>? EntidadesGubernamentales { get; set; }
    }
} 