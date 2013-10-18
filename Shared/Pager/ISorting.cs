namespace Shared.Pager
{
    using System.Collections.Generic;

    public interface ISorting
    {
        IEnumerable<ISortingItem> Items { get; }
    }
}