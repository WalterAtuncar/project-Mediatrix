namespace _SB_._MediatrixApi_._Aplicacion_.DTOs
{
    public class MenuJerarquicoDto
    {
        public List<CategoriaMenuDto> Categorias { get; set; } = new();
    }

    public class CategoriaMenuDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int TotalEntidades { get; set; }
        public List<EntidadMenuDto> Entidades { get; set; } = new();
    }

    public class EntidadMenuDto
    {
        public int EntidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? NombreEncargado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public CategoriaEntidadDto? Categoria { get; set; }
    }
} 