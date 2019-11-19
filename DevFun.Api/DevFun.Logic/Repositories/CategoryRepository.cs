using System;
using System.Linq;
using System.Linq.Expressions;
using _4tecture.DataAccess.Common.Repositories;
using _4tecture.DataAccess.Common.Storages;
using DevFun.Common.Entities;
using DevFun.Common.Repositories;

namespace DevFun.Logic.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
    {
        protected override Expression<Func<Category, int>> IdPropertyExpression => e => e.Id;

        public CategoryRepository(IStorage storage, IDetachedEntityMapperFactory mapperFactory)
            : base(storage, mapperFactory)
        {
        }

        protected override IQueryable<Category> ApplyDefaultIncludes(IQueryable<Category> query)
        {
            return query;
        }

        protected override Expression<Func<Category, bool>> DefineEntitySecurityFilterExpression(RepositoryAction action)
        {
            return e => true;
        }
    }
}