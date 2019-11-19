using System;
using System.Linq;
using System.Linq.Expressions;
using _4tecture.DataAccess.Common.Repositories;
using _4tecture.DataAccess.Common.Storages;
using DevFun.Common.Entities;
using DevFun.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFun.Logic.Repositories
{
    public class DevJokeRepository : RepositoryBase<DevJoke, int>, IDevJokeRepository
    {
        protected override Expression<Func<DevJoke, int>> IdPropertyExpression => e => e.Id;

        public DevJokeRepository(IStorage storage, IDetachedEntityMapperFactory mapperFactory)
            : base(storage, mapperFactory)
        {
        }

        protected override IQueryable<DevJoke> ApplyDefaultIncludes(IQueryable<DevJoke> query)
        {
            return query.Include(i => i.Category);
        }

        protected override Expression<Func<DevJoke, bool>> DefineEntitySecurityFilterExpression(RepositoryAction action)
        {
            return e => true;
        }
    }
}