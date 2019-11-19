using System;
using System.Linq;
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
    public class DevJokeService : IDevJokeService
    {
        private readonly IStorageFactory<IDevFunStorage> storageFactory;
        private readonly ILogger<DevJokeService> logger;

        public DevJokeService(
            IStorageFactory<IDevFunStorage> storageFactory,
            ILogger<DevJokeService> logger)
        {
            this.storageFactory = storageFactory ?? throw new ArgumentNullException(nameof(storageFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> GetCount()
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            var result = (await repo.GetAll().ConfigureAwait(false)).Count();
            return result;
        }

        public async Task<DevJoke> GetRandomJoke()
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            var result = (await repo.GetAll().ConfigureAwait(false)).OrderBy(r => Guid.NewGuid()).FirstOrDefault();
            return result;
        }

        public async Task<IPagedEnumerable<DevJoke>> GetAll(QueryFilter<DevJoke> queryFilter = null, TrackingBehavior trackingBehavior = TrackingBehavior.Tracking)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            return await repo.GetAll(queryFilter, trackingBehavior).ConfigureAwait(false);
        }

        public async Task<DevJoke> GetById(int id, TrackingBehavior trackingBehavior = TrackingBehavior.Tracking)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            return await repo.GetById(id, trackingBehavior).ConfigureAwait(false);
        }

        public async Task<DevJoke> Create(DevJoke joke)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            var result = await repo.AddDetached(joke).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }

        public async Task<DevJoke> Update(DevJoke joke)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            var result = await repo.UpdateDetached(joke).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }

        public async Task<DevJoke> Delete(int id)
        {
            using var session = storageFactory.CreateStorageSession();
            var repo = session.ResolveRepository<IDevJokeRepository>();
            var result = await repo.Delete(id).ConfigureAwait(false);
            await session.SaveChanges().ConfigureAwait(false);
            return result;
        }
    }
}