namespace Storage.Models.Paging
{
    public interface IPageableViewModel
    {
        int Page { get; set; }
        int PageSize { get; set; }

        int TotalRows { get; }
    }
}