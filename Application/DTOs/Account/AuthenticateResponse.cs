using Newtonsoft.Json;
using System;

namespace Application.DTOs.Account
{
    public class AuthenticateResponse
    {
        //public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public string JwtToken { get; set; }
        public DateTime Expires { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
