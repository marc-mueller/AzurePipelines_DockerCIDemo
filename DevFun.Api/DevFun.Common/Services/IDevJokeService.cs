using System.Threading.Tasks;
using _4tecture.DataAccess.Common.Services;
using DevFun.Common.Entities;

namespace DevFun.Common.Services
{
    public interface IDevJokeService : IEntityService<DevJoke, int>
    {
        Task<DevJoke> GetRandomJoke();

        Task<int> GetCount();
    }
}