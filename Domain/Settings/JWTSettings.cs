namespace Domain.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public double DurationInMinutes { get; set; }
        public int RefreshTokenTTL { get; set; }
        public string CookieName { get; set; }
    }
}
