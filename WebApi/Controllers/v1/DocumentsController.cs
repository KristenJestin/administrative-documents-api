using System.Net;
using System.Threading.Tasks;
using Application.Features.Documents.Commands.CreateDocument;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[Authorize]
	[ApiVersion("1.0")]
	public class DocumentsController : BaseApiController
	{

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateDocumentCommand command)
		{
			if (command == null)
				throw new ApiProblemDetailsException("Invalid JSON.", (int)HttpStatusCode.BadRequest);

			return Ok(await Mediator.Send(command));
		}
	}
}
