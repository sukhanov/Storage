using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Models.Paging
{
    public interface IPageableViewModel
    {
        int Page { get; set; }
        int PageSize { get; set; }

        int TotalRows { get; }
    }
}