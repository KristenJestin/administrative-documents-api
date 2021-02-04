using System.Collections.Generic;

namespace Domain.Common
{
    public class PaginatedList<T> where T : BaseEntity
    {
        public IEnumerable<T> Items { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }


        public PaginatedList(IEnumerable<T> lists, int count, int pageNumber, int pageSize)
        {
            Items = lists;
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
        }
    }
}
