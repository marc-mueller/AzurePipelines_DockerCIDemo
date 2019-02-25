using _4tecture.DataAccess.Common.Repositories;
using DevFun.Common.Repositories;
using System;
using System.Linq.Expressions;
using _4tecture.DataAccess.Common.Storages;
using DevFun.Common.Entities;
using System.Linq;

namespace DevFun.Logic.Repositories
{
    public class CategoryRepository : RepositoryBase<Category, int, Category>, ICategoryRepository
    {
        public CategoryRepository(IStorage storage) : base(storage)
        {
        }

        protected override Expression<Func<Category, bool>> GetPrimaryKeyFilterExpression(int keyValue)
        {
            return e => e.Id == keyValue;
        }

        protected override IQueryable<Category> ApplyDefaultIncludes(IQueryable<Category> query)
        {
            return query;
        }

        protected override int GetIdForDto(Category detachedEntity)
        {
            return detachedEntity.Id;
        }
    }
}
