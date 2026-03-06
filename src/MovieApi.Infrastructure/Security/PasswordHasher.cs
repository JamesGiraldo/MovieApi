using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MovieApi.Application.Abstractions.Auth;

namespace MovieApi.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));

        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    public bool Verify(string password, string passwordHash)
    {
        var parts = passwordHash.Split('.');
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));

        return hash == parts[1];
    }
}