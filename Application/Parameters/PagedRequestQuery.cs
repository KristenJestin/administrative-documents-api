using System;

namespace Application.Parameters
{
    public abstract class PagedRequestQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedRequestQuery()
        {
            Page = 1;
            PageSize = 10;
        }

        #region methods
        public virtual void ValidateValues()
        {
            Page = Math.Clamp(Page, 1, 1000);
            PageSize = PageSize < 1 || PageSize > 10 ? 10 : PageSize;
        }
        #endregion
    }
}
