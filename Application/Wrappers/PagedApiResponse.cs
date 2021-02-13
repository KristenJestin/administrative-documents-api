using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class PagedApiResponse<T>: BasicApiResponse<T>
    {
        public ApiResponsePagination Pagination { get; set; }

        public PagedApiResponse(T result = default, string message = "", ApiResponsePagination pagination = null) : base(result, message)
        {
            Message = message == string.Empty ? "Success" : message;
            Result = result;
            Pagination = pagination;
        }

        public PagedApiResponse(T result = default, ApiResponsePagination pagination = null) : base(result)
        {
            Message = "Request successful.";
            Result = result;
            Pagination = pagination;
        }
    }
}