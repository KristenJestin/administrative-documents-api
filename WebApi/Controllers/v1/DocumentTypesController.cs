using System.Threading.Tasks;
using Application.Features.Documents.Queries.GetAllDocumentTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentTypesController : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> Get()
			=> Ok(await Mediator.Send(new GetAllDocumentTypesQuery()));
	}
}
