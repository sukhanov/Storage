namespace Storage.Code
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Shared;
    using Models.Paging;

    public static class PagerExtensions
    {
        private const string DefaultPageParameterName = "Paging.Page";

        public static MvcHtmlString Pager(this HtmlHelper html, int currentPage, int pageSize, int totalRows, Func<int, string> url)
        {
            return Pager(html, currentPage, pageSize, totalRows, url, PagerSettings.Default);
        }

        private static MvcHtmlString RenderLink(string href, string text)
        {
            return RenderLink(href, text, false);
        }

        private static MvcHtmlString RenderLink(string href, string text, bool current)
        {
            return new MvcHtmlString(String.Format("<li class=\"{2}\"><a class=\"pager-item\" data-spa-nav=\"true\" title=\"{1}\" href=\"{0}\">{1}</a></li>", href, text, current ? "active" : ""));
        }

        public static MvcHtmlString Pager(this HtmlHelper html, int currentPage, int pageSize, int totalRows, Func<int, string> url, PagerSettings settings)
        {
            int blockSize = settings.PagesRangeSize;

            var pagesCount = (int)Math.Ceiling((double)totalRows / pageSize);
            var blocksCount = (int)Math.Ceiling((double)pagesCount / blockSize);
            var currentBlock = 0;

            for (var i = 0; i < blocksCount; i++)
            {
                var floor = i * blockSize + 1;
                var ceil = (i + 1) * blockSize;
                if ((currentPage < floor) || (currentPage > ceil)) continue;
                currentBlock = i;
                break;
            }

            var startPage = currentBlock * blockSize + 1; //first visible page
            var endPage = (startPage + blockSize - 1); //last visible page
            if (endPage > pagesCount)
            {
                endPage = pagesCount;
            }

            StringBuilder sb = new StringBuilder(1024);
            //sb.AppendFormat("<span class=\"text\">{0}:&nbsp;</span>", settings.Pages);

            //first page, prev page)
            if (currentPage > 1)
            {
                sb.Append(RenderLink(url(1), settings.FirstPage));
                sb.Append(RenderLink(url(currentPage - 1), settings.PrevPage));
            }

            //previous dots
            if (startPage > 1) //first page not visible
            {
                int prevPage = startPage - 1;
                sb.Append(RenderLink(url(prevPage), "..."));
            }
            //pages links
            for (int i = startPage; i <= endPage; i++)
            {
                //if (currentPage == i)
                //{
                //	sb.AppendFormat("<span class=\"pager-item\">{0}</span>", i);
                //}
                //else
                //{
                //	sb.Append(RenderLink(url(i), i.ToString()));
                //}
                sb.Append(RenderLink(url(i), i.ToString(), currentPage == i));
            }
            //final dots
            if (endPage < pagesCount) //last page not visible
            {
                int nextPage = endPage + 1;
                sb.Append(RenderLink(url(nextPage), "..."));
            }
            //next page, last page
            if (currentPage < pagesCount)
            {
                sb.Append(RenderLink(url(currentPage + 1), settings.NextPage));
                sb.Append(RenderLink(url(pagesCount), settings.LastPage));
            }

            return MvcHtmlString.Create("<ul class='pagination'>" + sb + "</ul>");
        }

        public static MvcHtmlString Pager(this HtmlHelper html, int currentPage, int pageSize, int totalRows)
        {
            return Pager(html, currentPage, pageSize, totalRows, x => GetDefaultPageUrl(x));
        }

        public static MvcHtmlString Pager(this HtmlHelper html, int currentPage, int pageSize, int totalRows, string paramName)
        {
            return Pager(html, currentPage, pageSize, totalRows, x => GetDefaultPageUrl(x, paramName));
        }

        public static MvcHtmlString Pager(this HtmlHelper html, IPageableViewModel model, PagerSettings settings)
        {
            return Pager(html, model.Page, model.PageSize, model.TotalRows, x => GetDefaultPageUrl(x), settings);
        }

        public static MvcHtmlString Pager(this HtmlHelper html, IPageableViewModel model, object parameters = null)
        {
            return Pager(html, model.Page, model.PageSize, model.TotalRows, x => GetDefaultPageUrl(x, parameters: parameters));
        }

        public static MvcHtmlString Pager(this HtmlHelper html, IPageableViewModel model, string paramName, object parameters = null)
        {
            return Pager(html, model.Page, model.PageSize, model.TotalRows, x => GetDefaultPageUrl(x, paramName, parameters));
        }

        private static string GetDefaultPageUrl(int page, string pageParameterName = DefaultPageParameterName,
            object parameters = null)
        {
            string url = HttpContext.Current.Request.Url.PathAndQuery;
            bool encodeUrl = HttpContext.Current.Request.Browser.IsIE();
            var helper = UriHelper.Create(url).AddParam(pageParameterName, page);

            var paramCollection = AnonymousObjectToDictionary(parameters);
            foreach (var parameter in paramCollection)
            {
                helper.AddParam(parameter.Key, parameter.Value);
            }

            //.RemoveParam("_")
            return helper.BuildUri(encodeUrl);
        }

        public static IDictionary<string, object> AnonymousObjectToDictionary(object htmlAttributes)
        {
            var result = new Dictionary<string, object>();

            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    result.Add(property.Name, property.GetValue(htmlAttributes));
                }
            }

            return result;
        }
    }
}
