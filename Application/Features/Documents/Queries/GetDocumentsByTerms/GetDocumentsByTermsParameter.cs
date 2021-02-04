using Application.Parameters;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsParameter : PagedRequestParameter
    {
        public string Term { get; set; }

        // TODO: add validations
        //public int? Type { get; set; }

        //public string Tag { get; set; }
    }
}
