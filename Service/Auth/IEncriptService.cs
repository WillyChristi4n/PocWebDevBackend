using PocWebDevBackend.Models;

namespace PocWebDevBackend.Service.Auth
{
    public interface IEncriptService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string givePassword);
    }
}