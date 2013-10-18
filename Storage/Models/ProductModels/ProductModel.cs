using Storage.Models.Paging;

namespace Storage.Models.ProductModels
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web.Mvc;
    using DomainObject;

    public class ProductModel : LayoutModel, IPageableViewModel
    {
        public ProductModel()
        {
            PageSize = 5;
        }

        public bool EditingMode { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }

        #region ' sort '

        public string SortColumn { get; set; }

        public SortOrder SortOrder { get; set; }

        public SortOrder GetNewOrder(string columnName)
        {
            if (columnName != SortColumn) return SortOrder.Ascending;
            if (SortOrder == SortOrder.Ascending) return SortOrder.Descending;
            return SortOrder.Ascending;
        }

        #endregion

        #region ' Paging '

        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalRows { get; set; }

        public int Skip
        {
            get { return (Page - 1) * PageSize; }
        }

        public int Take
        {
            get { return PageSize; }
        }

        public bool DisplayPager
        {
            get
            {
                return TotalRows > PageSize;
            }
        }

        #endregion Paging

        public string GetOrderSymbol(string columnName)
        {
            if (columnName != SortColumn) return string.Empty;
            if (SortOrder == SortOrder.Ascending) return @"\/";
            return @"/\";
        }
    }
}