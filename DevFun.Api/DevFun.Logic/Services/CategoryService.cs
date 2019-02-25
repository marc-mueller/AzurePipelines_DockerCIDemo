using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4tecture.DataAccess.Common.Storages;
using DevFun.Common.Entities;
using DevFun.Common.Repositories;
using DevFun.Common.Services;
using DevFun.Common.Storages;
using Microsoft.Extensions.Logging;

namespace DevFun.Logic.Services
{
    public class CategoryService : ICategoryService
    {
        public IStorageFactory<IDevFunStorage> StorageFactory { get; private set; }
        public ILogger Logger { get; private set; }

        public CategoryService(
            IStorageFactory<IDevFunStorage> storageFactory,
            ILogger<CategoryService> logger)
        {
            this.StorageFactory = storageFactory ?? throw new ArgumentNullException(nameof(storageFactory));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            using (var session = StorageFactory.CreateStorageSession())
            {
                var repo = session.ResolveRepository<ICategoryRepository>();
                var result = repo.GetAll().ToList();
                return Task.FromResult(result.AsEnumerable<Category>());
            }
        }

        public Task<Category> GetCategoryById(int id)
        {
            using (var session = StorageFactory.CreateStorageSession())
            {
                var repo = session.ResolveRepository<ICategoryRepository>();
                var result = repo.GetById(id);
                return Task.FromResult(result);
            }
        }

        public async Task<Category> Create(Category category)
        {
            using (var session = StorageFactory.CreateStorageSession())
            {
                var repo = session.ResolveRepository<ICategoryRepository>();
                var result = repo.AddDetached(category);
                await session.SaveChanges();
                return result;
            }
        }

        public async Task<Category> Update(Category category)
        {
            using (var session = StorageFactory.CreateStorageSession())
            {
                var repo = session.ResolveRepository<ICategoryRepository>();
                var result = repo.UpdateDetached(category);
                await session.SaveChanges();
                return result;
            }
        }

        public async Task<Category> Delete(int id)
        {
            using (var session = StorageFactory.CreateStorageSession())
            {
                var repo = session.ResolveRepository<ICategoryRepository>();
                var result = repo.Delete(id);
                await session.SaveChanges();
                return result;
            }
        }
    }
}