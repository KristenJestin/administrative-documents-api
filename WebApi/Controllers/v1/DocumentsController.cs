using System.Threading.Tasks;
using Application.Features.Documents.Commands.CreateDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentsController : BaseApiController
	{

		[HttpPost]
		public async Task<IActionResult> Post(CreateDocumentCommand command)
		{
			return Ok("test");
		}
	}
}
