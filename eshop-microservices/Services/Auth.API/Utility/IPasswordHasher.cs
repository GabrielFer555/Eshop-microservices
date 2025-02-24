namespace Auth.API.Utility
{
    public interface IPasswordHasher
    {
        string HashPassword(string hash);
        bool VerifyPassword(string password ,string hash);
    }
}
