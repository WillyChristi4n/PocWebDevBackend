using Microsoft.AspNetCore.Identity;
using PocWebDevBackend.Models;

namespace PocWebDevBackend.Service.Auth
{
    public class EncriptService: IEncriptService
    {
        public readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }
        public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
        {
            var matchesPassword = _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return matchesPassword == PasswordVerificationResult.Success;
        }
    }
}
