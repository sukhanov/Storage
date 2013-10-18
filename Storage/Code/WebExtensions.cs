using System;
using System.Web;

namespace Shared
{
    public static class WebExtensions
    {
        public static bool IsJson(this HttpRequest request)
        {
            return request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAjax(this HttpRequest request)
        {
            return HeaderIs(request, "X-Requested-With", "XMLHttpRequest");
        }

        public static bool IsMsAjax(this HttpRequest request)
        {
            return IsAjax(request) && HeaderIs(request, "x-microsoftajax", "delta=true");
        }

        public static bool IsIE(this HttpBrowserCapabilities browser)
        {
            return browser.Browser.ToLower().Equals("ie");
        }

        private static bool HeaderIs(HttpRequest request, string key, string value)
        {
            if (request != null)
            {
                return value.Equals(request.Headers.Get(key), StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}