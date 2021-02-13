using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public AccountRole Role { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified
            => Verified.HasValue || PasswordReset.HasValue;
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public List<AccountRefreshToken> RefreshTokens { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Account()
            => RefreshTokens = new List<AccountRefreshToken>();


        #region methods
        public bool OwnsToken(string token)
           => RefreshTokens?.Find(x => x.Token == token) != null;
        #endregion
    }
}