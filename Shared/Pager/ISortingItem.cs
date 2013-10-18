namespace Shared.Pager
{
    using System.Data.SqlClient;

    public interface ISortingItem
    {
        string Column { get; }
        SortOrder Order { get; }
    }
}