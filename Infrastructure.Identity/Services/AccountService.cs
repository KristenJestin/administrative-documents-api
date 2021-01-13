using Application.DTOs.Account;
using Application.Interfaces;
using AutoWrapper.Wrappers;
using Domain.Settings;
using Infrastructure.Identity.Helpers;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            // check if email exist
            if (user == null)
                throw new ApiProblemDetailsException($"No Accounts Registered with {request.Email}.", (int)HttpStatusCode.BadRequest);

            // check if password is valid
            SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new ApiProblemDetailsException($"Invalid Credentials for '{request.Email}'.", (int)HttpStatusCode.BadRequest);

            // check if email is confirmed
            if (!user.EmailConfirmed)
                throw new ApiProblemDetailsException($"Account Not Confirmed for '{request.Email}'.", (int)HttpStatusCode.BadRequest);

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            //IList<string> rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            //response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            RefreshToken refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;

            return response;
        }


        #region privates
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            List<Claim> roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            IEnumerable<Claim> claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
            => new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        #endregion
    }
}
