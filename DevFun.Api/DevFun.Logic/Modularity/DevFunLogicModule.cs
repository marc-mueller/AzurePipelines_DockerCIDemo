using _4tecture.DependencyInjection.Common;
using _4tecture.Modularity.Common;
using DevFun.Common.Repositories;
using DevFun.Common.Services;
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

            serviceCollection.AddTransient<IDevJokeService, DevJokeService>();
            serviceCollection.AddTransient<ICategoryService, CategoryService>();

            return serviceCollection;
        }
    }

    public static class ModuleCatalogExtensions
    {
        public static IModuleCatalog AddDevFunLogicModule(this IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule("DevFunLogicModule", new DevFunLogicModule());
            return moduleCatalog;
        }
    }
}