using _4tecture.DataAccess.Common.Extensions;
using _4tecture.DataAccess.Common.Repositories;
using _4tecture.DependencyInjection.Common;
using _4tecture.Modularity.Common;
using DevFun.Common.Dtos;
using DevFun.Common.Entities;
using DevFun.Common.Repositories;
using DevFun.Common.Services;
using DevFun.Logic.Mappers;
using DevFun.Logic.Repositories;
using DevFun.Logic.Services;

namespace DevFun.Logic.Modularity
{
    public class DevFunLogicModule : ModuleBase
    {
        public override IServiceDefinitionCollection RegisterServices(IServiceDefinitionCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDevJokeRepository, DevJokeRepository>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();

            serviceCollection.AddDefaultDetachedEntityMapper<Category>();
            serviceCollection.AddDefaultDetachedEntityMapper<DevJoke>();
            serviceCollection.AddSingleton<IDetachedEntityMapperFactory, DetachedEntityMapperFactory>();

            serviceCollection.AddDefaultEntityMapper<Category, CategoryDto, int>(c => c.Id, c => c.Id);
            serviceCollection.AddEntityMapper<DevJoke, DevJokeDto, DevJokeMappingDtoMapperConfiguration, int>(d => d.Id, d => d.Id);

            serviceCollection.AddTransient<IDevJokeService, DevJokeService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();

            serviceCollection.AddMapperFactories();

            return serviceCollection;
        }
    }

    public static class ModuleCatalogExtensions
    {
        public static IModuleCatalogCollection AddDevFunLogicModule(this IModuleCatalogCollection moduleCatalog)
        {
            if (moduleCatalog is null)
            {
                throw new System.ArgumentNullException(nameof(moduleCatalog));
            }

            moduleCatalog.AddModule("DevFunLogicModule", new DevFunLogicModule());
            return moduleCatalog;
        }
    }
}