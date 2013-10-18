using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Shared
{
    public class UriHelper
    {
        private string _path = String.Empty;

        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>(20, StringComparer.OrdinalIgnoreCase);

        public UriHelper(string uri)
        {
            if (uri == null)
            {
                throw new NullReferenceException();
            }
            ParseUri(uri);
        }

        private void ParseUri(string uri)
        {
            string[] parts = uri.Split('?');
            if (parts.Length == 0)
            {
                return;
            }

            _path = parts[0];

            if (parts.Length > 1)
            {
                NameValueCollection coll = HttpUtility.ParseQueryString(parts[1]);
                foreach (var key in coll.AllKeys)
                {
                    if (!_parameters.ContainsKey(key))
                    {
                        _parameters.Add(key, coll[key]);
                    }
                }
            }
        }

        public static UriHelper Create(string uri)
        {
            return new UriHelper(uri);
        }

        public UriHelper AddParam(string key, object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
            }
            else
            {
                _parameters.Add(key, value);
            }
            return this;
        }

        public UriHelper RemoveParam(string key)
        {
            _parameters.Remove(key);
            return this;
        }

        public string BuildUri()
        {
            return BuildUri(false);
        }

        public string BuildUri(bool encodeComponents)
        {
            string query = BuildQuery(_parameters, encodeComponents);
            if (String.IsNullOrEmpty(query))
            {
                return _path;
            }
            return String.Format("{0}?{1}", _path, query);
        }

        public string GetPath()
        {
            return _path;
        }

        public string GetQuery()
        {
            return GetQuery(false);
        }

        public string GetQuery(bool encodeComponents)
        {
            return BuildQuery(_parameters, encodeComponents);
        }

        private static string BuildQuery(IDictionary parameters, bool encodeComponents)
        {
            StringBuilder buff = new StringBuilder(1024);
            bool amp = false;
            foreach (DictionaryEntry entry in parameters)
            {
                if (amp)
                {
                    buff.Append("&");
                }
                string key = entry.Key.ToString();
                string value = entry.Value != null ? entry.Value.ToString() : String.Empty;
                value = encodeComponents ? HttpUtility.UrlEncode(value, Encoding.UTF8) : value;
                buff.AppendFormat("{0}={1}", key, value);
                amp = true;
            }
            return buff.ToString();
        }

        public override string ToString()
        {
            return BuildUri();
        }

    }
}