using Microsoft.AspNetCore.Identity;
using PocWebDevBackend.Models;

namespace PocWebDevBackend.Service.Auth
{
    public class EncriptService : IEncriptService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string givenPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, givenPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
