using Application.DTOs.Account;
using Application.Interfaces.Services;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Entities;
using Domain.Settings;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly IdentityDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountService(IdentityDbContext context, IOptions<JwtSettings> jwtSettings, IMapper mapper)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, string ipAddress)
        {
            Account account = await _context.Accounts.SingleOrDefaultAsync(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BC.Verify(model.Password, account.PasswordHashed))
                throw new ApiProblemDetailsException("Username or password is incorrect", StatusCodes.Status401Unauthorized); ;

            // authentication successful so generate jwt and refresh tokens
            string jwtToken = GenerateJwtToken(account);
            var refreshToken = GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from account
            RemoveOldRefreshTokens(account);
            // save changes to db
            _context.Update(account);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            response.Expires = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);
            return response;
        }

        public async Task<AuthenticateResponse> RefreshTokenAsync(string token, string ipAddress)
        {
            (AccountRefreshToken refreshToken, Account account) = await GetRefreshTokenAsync(token);

            // replace old refresh token with a new one and save
            AccountRefreshToken newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from account
            RemoveOldRefreshTokens(account);
            // save changes to db
            _context.Update(account);
            await _context.SaveChangesAsync();

            // generate new jwt
            string jwtToken = GenerateJwtToken(account);

            AuthenticateResponse response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            response.Expires = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);
            return response;
        }
        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            (AccountRefreshToken refreshToken, Account account) = await GetRefreshTokenAsync(token);

            // revoke token and save
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            _context.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> RegisterAsync(RegisterRequest model)
        {
            // validate
            if (await _context.Accounts.AnyAsync(x => x.Email == model.Email))
                throw new ApiProblemDetailsException("Email already exists.", StatusCodes.Status422UnprocessableEntity);

            if (await _context.Accounts.AnyAsync(x => x.Username == model.Username))
                throw new ApiProblemDetailsException("Username already exists.", StatusCodes.Status422UnprocessableEntity);

            // map model to new account object
            Account account = _mapper.Map<Account>(model);
            account.Role = AccountRole.Basic;
            account.CreatedAt = DateTime.UtcNow;
            account.VerificationToken = RandomTokenString();

            // hash password
            account.PasswordHashed = BC.HashPassword(model.Password);

            // save account
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }



        #region privates
        private string GenerateJwtToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            string ipAddress = IpHelper.GetIpAddress();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", account.Id.ToString()),
                    new Claim("email", account.Email),
                    new Claim("ip", ipAddress)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private AccountRefreshToken GenerateRefreshToken(string ipAddress)
            => new AccountRefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        private string RandomTokenString()
        {
            using RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return Convert.ToBase64String(randomBytes);
        }

        private void RemoveOldRefreshTokens(Account account)
            => account.RefreshTokens.RemoveAll(rt => rt.IsActive && rt.CreatedAt.AddDays(_jwtSettings.RefreshTokenTTL) <= DateTime.UtcNow);

        private async Task<(AccountRefreshToken, Account)> GetRefreshTokenAsync(string token)
        {
            Account account = await _context.Accounts
                .Include(a => a.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (account == null)
                throw new ApiProblemDetailsException("Invalid token", StatusCodes.Status401Unauthorized);

            AccountRefreshToken refreshToken = account.RefreshTokens.SingleOrDefault(x => x.Token == token);
            if (refreshToken == null || !refreshToken.IsActive)
                throw new ApiProblemDetailsException("Invalid token", StatusCodes.Status401Unauthorized);

            return (refreshToken, account);
        }
        #endregion
    }
}
