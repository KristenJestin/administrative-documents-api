using System.Threading.Tasks;
using Application.Features.DocumentTypes.Queries.GetAllDocumentTypes;
using Application.Features.DocumentTypes.Queries.GetDocumentTypeById;
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

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
			=> Ok(await Mediator.Send(new GetDocumentTypeByIdQuery { Id = id }));
	}
}
