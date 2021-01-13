using Application.DTOs.Account;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    }
}
