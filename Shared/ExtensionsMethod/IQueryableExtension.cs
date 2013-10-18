namespace Shared.ExtensionsMethod
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Dynamic;
    using Pager;

    public static class QueryableExtension
    {
        public static IQueryable<TE> PageBy<TE>(this IQueryable<TE> query, IPaging paging)
        {
            if (paging != null)
            {
                query = query.PageBy(paging.Skip, paging.Take);
            }
            return query;
        }

        public static IQueryable<TE> PageBy<TE>(this IQueryable<TE> query, int skip, int take)
        {
            if (skip > 0)
            {
                query = query.Skip(skip);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }
            return query;
        }

        public static IQueryable<TE> OrderBy<TE>(this IQueryable<TE> query, ISortingItem sortingItem)
        {
            if (sortingItem == null)
            {
                throw new ArgumentNullException("sortingItem");
            }

            if (String.IsNullOrEmpty(sortingItem.Column))
            {
                return query;
            }

            var sortExpression = string.Format("{0} {1}", sortingItem.Column, sortingItem.Order);
            try
            {
                return query.OrderBy(sortExpression);
            }
            catch (ParseException)
            {
                return query;
            }
        }


        public static IQueryable<TE> GetFiltered<TE>(this IQueryable<TE> queryable, Filter filter)
        {
            return queryable.OrderBy(filter).PageBy(filter);
        }

        public static IQueryable<TE> OrderBy<TE>(this IQueryable<TE> query, ISorting sorting)
        {
            if (sorting != null && sorting.Items != null && sorting.Items.Any(u => u.Order != SortOrder.Unspecified))
            {
                foreach (var sortSettingsItem in sorting.Items)
                {
                    query = OrderBy(query, sortSettingsItem);
                }
                return query;
            }
            return query.OrderBy(new SortItem { Column = "Id", Order = SortOrder.Ascending });
        }

    }
}
