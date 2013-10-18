namespace Shared.Pager
{
    using System.Data.SqlClient;

    public class SortItem : ISortingItem
    {
        public string Column { get; set; }
        public SortOrder Order { get; set; }
    }
}