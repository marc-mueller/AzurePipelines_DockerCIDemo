using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevFun.DataInitializer.Dtos;
using Newtonsoft.Json;

namespace DevFun.DataInitializer
{
    public class DevFunService
    {
        private readonly HttpClient client;

        private const string apiCategoryController = "/api/category";

        private const string apiJokesController = "/api/jokes";

        public DevFunService(Uri serviceHostBaseUri)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            this.client = new HttpClient(handler);
            client.BaseAddress = serviceHostBaseUri ?? throw new ArgumentNullException(nameof(serviceHostBaseUri));
            client.Timeout = new TimeSpan(0, 0, 10);
        }

        public async Task<IEnumerable<JokeDto>> GetJokes()
        {
            try
            {
                var json = await client.GetStringAsync($"{apiJokesController}");
                IEnumerable<JokeDto> jokes = JsonConvert.DeserializeObject<List<JokeDto>>(json);
                return jokes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading jokes: {ex.ToString()}");
            }

            return new List<JokeDto>();
        }

        public async Task<JokeDto> GetJoke(int id)
        {
            try
            {
                var json = await client.GetStringAsync($"{apiJokesController}/{id}");
                JokeDto joke = JsonConvert.DeserializeObject<JokeDto>(json);
                return joke;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading jokes: {ex.ToString()}");
            }

            return null;
        }

        public async Task<JokeDto> AddJoke(JokeDto jokeDto)
        {
            try
            {
                var jokeData = JsonConvert.SerializeObject(jokeDto);
                var response = await client.PostAsync($"{apiJokesController}", new StringContent(jokeData, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                JokeDto joke = JsonConvert.DeserializeObject<JokeDto>(contents);
                return joke;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding jokes: {ex.ToString()}");
            }

            return null;
        }

        public async Task<JokeDto> UpdateJoke(JokeDto jokeDto)
        {
            try
            {
                var jokeData = JsonConvert.SerializeObject(jokeDto);
                var response = await client.PutAsync($"{apiJokesController}/{jokeDto.Id}", new StringContent(jokeData, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                JokeDto joke = JsonConvert.DeserializeObject<JokeDto>(contents);
                return joke;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating jokes: {ex.ToString()}");
            }

            return null;
        }

        public async Task<JokeDto> DeleteJoke(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"{apiJokesController}/{id}");
                var contents = await response.Content.ReadAsStringAsync();
                JokeDto joke = JsonConvert.DeserializeObject<JokeDto>(contents);
                return joke;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting jokes: {ex.ToString()}");
            }

            return null;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            try
            {
                var json = await client.GetStringAsync($"{apiCategoryController}");
                IEnumerable<CategoryDto> categories = JsonConvert.DeserializeObject<List<CategoryDto>>(json);
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading categories: {ex.ToString()}");
            }

            return new List<CategoryDto>();
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            try
            {
                var json = await client.GetStringAsync($"{apiCategoryController}/{id}");
                CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(json);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading category: {ex.ToString()}");
            }

            return null;
        }

        public async Task<CategoryDto> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                var categoryData = JsonConvert.SerializeObject(categoryDto);
                var response = await client.PostAsync($"{apiCategoryController}", new StringContent(categoryData, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(contents);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding category: {ex.ToString()}");
            }

            return null;
        }

        public async Task<CategoryDto> UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var categoryData = JsonConvert.SerializeObject(categoryDto);
                var response = await client.PutAsync($"{apiCategoryController}/{categoryDto.Id}", new StringContent(categoryData, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(contents);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating category: {ex.ToString()}");
            }

            return null;
        }

        public async Task<CategoryDto> DeleteCategory(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"{apiCategoryController}/{id}");
                var contents = await response.Content.ReadAsStringAsync();
                CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(contents);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting category: {ex.ToString()}");
            }

            return null;
        }
    }
}