using System;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Interfaces.Services;
using Application.Wrappers;
using AutoWrapper.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly JwtSettings _jwtSettings;

		public AccountsController(IAccountService accountService, IOptions<JwtSettings> jwtSettings)
		{
			_accountService = accountService;
			_jwtSettings = jwtSettings.Value;
		}

		[HttpPost("authenticate")]
		public async Task<BasicApiResponse<AuthenticateResponse>> AuthenticateAsync([FromBody] AuthenticateRequest request)
		{
			if (!ModelState.IsValid)
				throw new ApiProblemDetailsException(ModelState);

			AuthenticateResponse response = await _accountService.AuthenticateAsync(request, GenerateIPAddress());
			setTokenCookie(response.RefreshToken);
			return new BasicApiResponse<AuthenticateResponse>(response);
		}

		[HttpPost("refresh-token")]
		public async Task<BasicApiResponse<AuthenticateResponse>> RefreshToken()
		{
			string refreshToken = Request.Cookies[_jwtSettings.CookieName];

			AuthenticateResponse response = await _accountService.RefreshTokenAsync(refreshToken, GenerateIPAddress());
			setTokenCookie(response.RefreshToken);
			return new BasicApiResponse<AuthenticateResponse>(response);
		}

		// TODO: add revoke token action



		#region privates
		private string GenerateIPAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For"))
				return Request.Headers["X-Forwarded-For"];
			else
				return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}

		private void setTokenCookie(string token)
		{
			CookieOptions cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(7)
			};
			Response.Cookies.Append(_jwtSettings.CookieName, token, cookieOptions);
		}
		#endregion
	}
}
