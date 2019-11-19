using System.Threading.Tasks;
using _4tecture.AspNetCoreExtensions.Controllers;
using _4tecture.DataAccess.Common.DtoMapping;
using DevFun.Common.Dtos;
using DevFun.Common.Entities;
using DevFun.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevFun.Api.Controllers
{
    public class JokesController : EntityCrudControllerBase<DevJoke, DevJokeDto, int, IDevJokeService>
    {
        public JokesController(IDevJokeService service, IMapperFactory mapperFactory)
            : base(service, mapperFactory)
        {
        }

        // GET api/jokes/random
        [HttpGet("random")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(nameof(GetRandom))]
        public async Task<ActionResult<DevJokeDto>> GetRandom()
        {
            return this.Mapper.MapToDto(await Service.GetRandomJoke().ConfigureAwait(false));
        }
    }
}