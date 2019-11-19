using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevFun.DataInitializer.Dtos;
using Newtonsoft.Json;

namespace DevFun.DataInitializer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "ok for sample")]
    public class DevFunService : IDisposable
    {
        private readonly HttpClient client;

        private const string apiCategoryController = "/api/v1.0/category";

        private const string apiJokesController = "/api/v1.0/jokes";

        public DevFunService(Uri serviceHostBaseUri)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            this.client = new HttpClient(handler);
            client.BaseAddress = serviceHostBaseUri ?? throw new ArgumentNullException(nameof(serviceHostBaseUri));
            client.Timeout = new TimeSpan(0, 0, 10);
        }

        public async Task<IEnumerable<JokeDto>> GetJokes()
        {
            try
            {
                var json = await client.GetStringAsync($"{apiJokesController}").ConfigureAwait(false);
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
                var json = await client.GetStringAsync($"{apiJokesController}/{id}").ConfigureAwait(false);
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
                var response = await client.PostAsync($"{apiJokesController}", new StringContent(jokeData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            if (jokeDto is null)
            {
                throw new ArgumentNullException(nameof(jokeDto));
            }

            try
            {
                var jokeData = JsonConvert.SerializeObject(jokeDto);
                var response = await client.PutAsync($"{apiJokesController}/{jokeDto.Id}", new StringContent(jokeData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                var response = await client.DeleteAsync($"{apiJokesController}/{id}").ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                var json = await client.GetStringAsync($"{apiCategoryController}").ConfigureAwait(false);
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
                var json = await client.GetStringAsync($"{apiCategoryController}/{id}").ConfigureAwait(false);
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
                var response = await client.PostAsync($"{apiCategoryController}", new StringContent(categoryData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            if (categoryDto is null)
            {
                throw new ArgumentNullException(nameof(categoryDto));
            }

            try
            {
                var categoryData = JsonConvert.SerializeObject(categoryDto);
                var response = await client.PutAsync($"{apiCategoryController}/{categoryDto.Id}", new StringContent(categoryData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                var response = await client.DeleteAsync($"{apiCategoryController}/{id}").ConfigureAwait(false);
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(contents);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting category: {ex.ToString()}");
            }

            return null;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.client?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}