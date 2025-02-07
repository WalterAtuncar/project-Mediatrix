namespace _SB_._MediatrixApi_._Aplicacion_.DTOs
{
    public class EntidadGubernamentalDto
    {
        public int EntidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string? NombreEncargado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstaEliminado { get; set; }
        public CategoriaEntidadDto? Categoria { get; set; }
    }

    public class CategoriaEntidadDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
} 