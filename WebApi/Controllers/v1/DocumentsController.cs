using System.Threading.Tasks;
using Application.DTOs.Document;
using Application.Features.Documents.Commands.CreateDocument;
using Application.Features.Documents.Queries.DownloadDocumentById;
using Application.Features.Documents.Queries.GetAllDocuments;
using Application.Features.Documents.Queries.GetDocumentById;
using Application.Features.Documents.Queries.GetDocumentsByTerms;
using Application.Wrappers;
using AutoWrapper.Filters;
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

		[HttpGet("{id}:int")]
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

		#region actions
		[HttpGet("{id}/[action]")]
		[AutoWrapIgnore]
		public async Task<IActionResult> Download(int id)
		{
			DownloadDocumentResponse download = await Mediator.Send(new DownloadDocumentByIdQuery { Id = id });
			return File(download.FileContent, download.ContentType, download.Name);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Latest([FromQuery] GetAllDocumentsParameter filter)
			=> Ok(await Mediator.Send(new GetAllDocumentsQuery() { PageSize = filter.PageSize, Page = filter.Page }));

		[HttpGet("[action]")]
		public async Task<IActionResult> Search([FromQuery] GetDocumentsByTermsParameter filter)
			=> Ok(await Mediator.Send(new GetDocumentsByTermsQuery { PageSize = filter.PageSize, Page = filter.Page, Term = filter.Term }));
		#endregion
	}
}
