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
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        // GET api/category
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await this.categoryService.GetCategories();
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await this.categoryService.GetCategoryById(id);
        }

        // POST api/category
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Category value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            try
            {
                var category = await categoryService.Create(value);
                if (category == null)
                {
                    return BadRequest();
                }

                return CreatedAtRoute(new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Category value)
        {
            if (value == null || value.Id != id)
            {
                return BadRequest();
            }

            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var updatedPkData = await categoryService.Update(value);
            return new ObjectResult(updatedPkData);
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            await categoryService.Delete(id);
            return new NoContentResult();
        }
    }
}