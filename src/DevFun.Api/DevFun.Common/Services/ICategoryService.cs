using System.Collections.Generic;
using System.Threading.Tasks;
using DevFun.Common.Entities;

namespace DevFun.Common.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();

        Task<Category> GetCategoryById(int id);

        Task<Category> Create(Category category);

        Task<Category> Update(Category category);

        Task<Category> Delete(int id);
    }
}