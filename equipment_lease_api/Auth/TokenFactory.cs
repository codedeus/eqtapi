using equipment_lease_api.Interfaces;
using System;
using System.Security.Cryptography;


namespace equipment_lease_api.Auth
{
    public sealed class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size=32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
