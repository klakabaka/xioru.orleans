﻿using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xioru.Grain.Contracts.Account;

namespace Xioru.Grain.Account
{
    public partial class AccountGrain
    {
        // https://www.c-sharpcorner.com/article/jwt-validation-and-authorization-in-net-5-0/
        public Task<Token> Login(string password)
        {
            if (!State.IsConfirmed)
            {
                var msg = $"Account '{AccountId}' not confirmed";
                _log.LogError(msg);
                throw new InvalidOperationException(msg);
            }

            if (State.Password != password)
            {
                var msg = $"Bad password for account '{AccountId}'";
                _log.LogError(msg);
                throw new InvalidOperationException(msg);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authConfig.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", AccountId) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _authConfig.Issuer,
                Audience = _authConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);

            return Task.FromResult(new Token
            {
                AccessToken = tokenStr
            });
        }

        public Task<Token> RefreshToken(string refreshToken)
        {
            if (!State.IsConfirmed)
            {
                var msg = $"Account '{AccountId}' not confirmed";
                _log.LogError(msg);
                throw new InvalidOperationException(msg);
            }

            throw new NotImplementedException();
        }
    }
}
