using Domain.Common;
using System;

namespace Application.Wrappers
{
    public class ApiResponsePagination
    {
        public int PageSize { get; private set; }
        public int Page { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasNextPage
            => Page < TotalPages;

        #region statics
        public static ApiResponsePagination Build<T>(PaginatedList<T> paginatedList) where T : BaseEntity
            => new ApiResponsePagination
            {
                PageSize = paginatedList.PageSize,
                Page = paginatedList.CurrentPage,
                TotalPages = (int)Math.Ceiling(paginatedList.TotalCount / (double)paginatedList.PageSize),
            };
        #endregion
    }
}
