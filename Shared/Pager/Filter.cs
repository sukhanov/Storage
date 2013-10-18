namespace Shared.Pager
{
    using System.Collections.Generic;

    public class Filter : ISorting, IPaging
    {
        public Filter(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }



        public IEnumerable<ISortingItem> Items { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}