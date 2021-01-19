using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class AuthenticationRequest
    {
        [Required]
        [JsonProperty("Username")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
