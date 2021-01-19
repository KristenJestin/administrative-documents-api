namespace Application.Parameters
{
    public class PagedRequestParameter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedRequestParameter()
        {
            Page = 1;
            this.PageSize = 15;
        }
        public PagedRequestParameter(int page, int pageSize)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
