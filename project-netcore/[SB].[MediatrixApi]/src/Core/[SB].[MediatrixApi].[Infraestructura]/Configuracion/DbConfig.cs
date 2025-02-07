namespace _SB_._MediatrixApi_._Infraestructura_.Configuracion
{
    public static class DbConfig
    {
        public static string GetConnectionString()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MediatrixDb.db");
            return $"Data Source={dbPath}";
        }
    }
} 