using Microsoft.AspNetCore.Identity;
using PocWebDevBackend.Models;

namespace PocWebDevBackend.Service.Auth
{
    public class EncriptService : IEncriptService
    {
        private readonly PasswordHasher<User> _hasher = new();

        string IEncriptService.HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        bool IEncriptService.VerifyPassword(User user, string hashedPassword, string givePassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, givePassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
