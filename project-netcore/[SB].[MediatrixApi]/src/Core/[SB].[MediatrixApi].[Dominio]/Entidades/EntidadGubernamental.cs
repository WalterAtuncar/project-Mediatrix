namespace _SB_._MediatrixApi_._Dominio_.Entidades
{
    public class EntidadGubernamental
    {
        public int EntidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Siglas { get; set; }
        public int CategoriaId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? NombreEncargado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstaEliminado { get; set; }

        // Propiedad de navegación
        public virtual CategoriaEntidad? Categoria { get; set; }
    }
} 