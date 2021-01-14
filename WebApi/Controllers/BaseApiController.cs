using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("v{version:apiVersion}/[controller]")]
	public abstract class BaseApiController : ControllerBase
	{
	}
}
