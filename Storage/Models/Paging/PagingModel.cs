namespace Storage.Models.Paging
{
    public abstract class PagingModel : IPageableViewModel
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalRows { get; set; }

        protected PagingModel()
        {
            PageSize = 10;
        }

        public int Skip
        {
            get { return (Page - 1) * PageSize; }
        }

        public int Take
        {
            get { return PageSize; }
        }
    }
}