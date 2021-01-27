using System.Net;
using System.Threading.Tasks;
using Application.DTOs.Document;
using Application.Features.Documents.Commands.CreateDocument;
using Application.Features.Documents.Queries.GetAllDocuments;
using Application.Features.Documents.Queries.GetDocumentById;
using Application.Wrappers;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentsController : BaseApiController
	{
		[HttpGet("[action]")]
		public async Task<IActionResult> Latest([FromQuery] GetAllDocumentsParameter filter)
			=> Ok(await Mediator.Send(new GetAllDocumentsQuery() { PageSize = filter.PageSize, Page = filter.Page }));

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
			=> Ok(await Mediator.Send(new GetDocumentByIdQuery { Id = id }));

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateDocumentCommand command)
		{
			if (command == null)
				throw new ApiProblemDetailsException("Invalid JSON.", StatusCodes.Status400BadRequest);

			BasicApiResponse<ReadDocumentResponse> response = await Mediator.Send(command);
			return CreatedAtAction(nameof(Get), new { id = response.Result.Id }, response);
		}
	}
}
