using System;
using System.Threading.Tasks;
using _4tecture.DataAccess.Common.Repositories;
using _4tecture.DataAccess.Common.Storages;
using DevFun.Common.Entities;
using DevFun.Common.Repositories;
using DevFun.Common.Services;
using DevFun.Common.Storages;
using Microsoft.Extensions.Logging;

namespace DevFun.Logic.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "because of the using and Task<>, the object is disposed too early when not using async/await in combination with using")]
    public class CategoryService : ICategoryService
    {
        private readonly IStorageFactory<IDevFunStorage> storageFactory;
        private readonly ILogger<CategoryService> logger;

        public CategoryService(
            IStorageFactory<IDevFunStorage> storageFactory,
            ILogger<CategoryService> logger)
        {
            this.storageFactory = storageFactory ?? throw new ArgumentNullException(nameof(storageFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IPagedEnumerable<Category>> GetAll(QueryFilter<Category> queryFilter = null, TrackingBehavior trackingBehavior = TrackingBehavior.Tracking)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<ICategoryRepository>();
            return await repo.GetAll(queryFilter, trackingBehavior).ConfigureAwait(false);
        }

        public async Task<Category> GetById(int id, TrackingBehavior trackingBehavior = TrackingBehavior.Tracking)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<ICategoryRepository>();
            return await repo.GetById(id, trackingBehavior).ConfigureAwait(false);
        }

        public async Task<Category> Create(Category category)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<ICategoryRepository>();
            var result = await repo.AddDetached(category).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }

        public async Task<Category> Update(Category category)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<ICategoryRepository>();
            var result = await repo.UpdateDetached(category).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }

        public async Task<Category> Delete(int id)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<ICategoryRepository>();
            var result = await repo.Delete(id).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }
    }
}