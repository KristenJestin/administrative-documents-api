using System;

namespace Domain.Entities
{
    public class AccountRefreshToken
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired
            => DateTime.UtcNow >= Expires;
        public DateTime? RevokedAt { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive
            => RevokedAt == null && !IsExpired;
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
    }
}
