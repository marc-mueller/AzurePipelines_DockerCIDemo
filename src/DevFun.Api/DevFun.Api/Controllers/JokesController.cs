using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevFun.Common.Entities;
using DevFun.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevFun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : Controller
    {
        public IDevJokeService DevJokeService { get; private set; }

        public JokesController(IDevJokeService devJokeService)
        {
            this.DevJokeService = devJokeService ?? throw new ArgumentNullException(nameof(devJokeService));
        }

        // GET api/jokes
        [HttpGet]
        public async Task<IEnumerable<DevJoke>> Get()
        {
            return await this.DevJokeService.GetJokes();
        }

        // GET api/jokes/random
        [HttpGet("random")]
        public async Task<DevJoke> GetRandom()
        {
            return await this.DevJokeService.GetRandomJoke();
        }

        // GET api/jokes/5
        [HttpGet("{id}")]
        public async Task<DevJoke> Get(int id)
        {
            return await this.DevJokeService.GetJokeById(id);
        }

        // POST api/jokes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]DevJoke value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            try
            {
                var joke = await DevJokeService.Create(value);
                if (joke == null)
                {
                    return BadRequest();
                }

                return CreatedAtRoute(new { id = joke.Id }, joke);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/jokes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DevJoke value)
        {
            if (value == null || value.Id != id)
            {
                return BadRequest();
            }

            var joke = await DevJokeService.GetJokeById(id);
            if (joke == null)
            {
                return NotFound();
            }

            var updatedPkData = await DevJokeService.Update(value);
            return new ObjectResult(updatedPkData);
        }

        // DELETE api/jokes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var joke = await DevJokeService.GetJokeById(id);
            if (joke == null)
            {
                return NotFound();
            }

            await DevJokeService.Delete(id);
            return new NoContentResult();
        }
    }
}