namespace _SB_._MediatrixApi_._Servicios_.Auth
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
} 