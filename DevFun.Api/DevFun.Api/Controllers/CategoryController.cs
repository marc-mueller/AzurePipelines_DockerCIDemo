using _4tecture.AspNetCoreExtensions.Controllers;
using _4tecture.DataAccess.Common.DtoMapping;
using DevFun.Common.Dtos;
using DevFun.Common.Entities;
using DevFun.Common.Services;

namespace DevFun.Api.Controllers
{
    public class CategoryController : EntityCrudControllerBase<Category, CategoryDto, int, ICategoryService>
    {
        public CategoryController(ICategoryService service, IMapperFactory mapperFactory)
            : base(service, mapperFactory)
        {
        }
    }
}