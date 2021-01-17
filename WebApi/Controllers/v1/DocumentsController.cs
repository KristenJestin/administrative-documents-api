using System.Net;
using System.Threading.Tasks;
using Application.Features.Documents.Commands.CreateDocument;
using Application.Features.Documents.Queries.GetDocumentById;
using AutoWrapper.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentsController : BaseApiController
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
			=> Ok(await Mediator.Send(new GetDocumentByIdQuery { Id = id }));

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateDocumentCommand command)
		{
			if (command == null)
				throw new ApiProblemDetailsException("Invalid JSON.", (int)HttpStatusCode.BadRequest);

			Document document = await Mediator.Send(command);
			return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
		}
	}
}
