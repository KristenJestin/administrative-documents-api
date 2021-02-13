using System.Threading.Tasks;
using Application.Features.DocumentTags.Queries.GetAllDocumentTags;
using Application.Features.DocumentTags.Queries.GetDocumentTagById;
using Application.Features.DocumentTags.Queries.GetDocumentTagBySlug;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentTagsController : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> Get()
			=> Ok(await Mediator.Send(new GetAllDocumentTagsQuery()));

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
			=> Ok(await Mediator.Send(new GetDocumentTagByIdQuery { Id = id }));


		[HttpGet("{slug:regex(^[[a-z0-9]]+(?:-[[a-z0-9]]+)*$)}")]
		public async Task<IActionResult> Get(string slug)
			=> Ok(await Mediator.Send(new GetDocumentTagBySlugQuery { Slug = slug }));
	}
}
