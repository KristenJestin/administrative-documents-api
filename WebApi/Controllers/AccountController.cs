using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("authenticate")]
		public async Task<AuthenticationResponse> AuthenticateAsync([FromBody] AuthenticationRequest request)
		{
			if (ModelState.IsValid)
				return await _accountService.AuthenticateAsync(request, GenerateIPAddress());

			throw new ApiProblemDetailsException(ModelState);
		}


		#region privates
		private string GenerateIPAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For"))
				return Request.Headers["X-Forwarded-For"];
			else
				return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}
		#endregion
	}
}
