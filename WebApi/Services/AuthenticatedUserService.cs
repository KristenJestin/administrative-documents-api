using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebApi.Services
{
	public class AuthenticatedUserService : IAuthenticatedUserService
	{
		public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
		{
			string value = httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

			if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int id))
			{
				UserId = id;
			}
		}

		public int UserId { get; }
	}
}
