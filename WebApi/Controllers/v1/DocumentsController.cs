using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentsController : BaseApiController
	{

		[HttpPost]
		public async Task<IActionResult> Post(/*CreateProductCommand command*/)
		{
			return Ok("");
		}
	}
}
