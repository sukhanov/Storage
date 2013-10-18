using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using DomainObject;
using Shared;

namespace Repository
{
    using Shared.ExtensionsMethod;
    using Shared.Pager;

    public class Repository<TE> where TE : EntityObject
    {
        protected readonly DataBase Db = new DataBase();

        public IQueryable<TE> Query()
        {
            return Db.CreateObjectSet<TE>();
        }

        public IEnumerable<TE> Query(Expression<Func<TE, bool>> func, Filter filter)
        {
            return Query().Where(func).GetFiltered(filter).ToList();
        }

        public IEnumerable<TE> Query(Expression<Func<TE, bool>> func)
        {
            return Query().Where(func);
        }

        public int GetTotalCount(Expression<Func<TE, bool>> func)
        {
            return Query(func).Count();
        }
    }


}
