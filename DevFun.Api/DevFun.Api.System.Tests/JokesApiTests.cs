using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevFun.Api.System.Tests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DevFun.Api.System.Tests
{
    [TestClass]
    public class JokesApiTests : TestBase
    {
        private const string baseApiUrl = "api/V1.0/jokes";

        [TestMethod]
        public async Task DevFun_GetJokes_OperationsSuccessful()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            // Act
            var initialJokes = await GetJokes(client).ConfigureAwait(false);

            // Assert
            Assert.IsNotNull(initialJokes);
            Assert.AreNotEqual(0, initialJokes.Count());
        }

        [TestMethod]
        public async Task DevFun_GetRandomJoke_ResultIsRandom()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            // Act
            var randomJoke1 = JsonConvert.DeserializeObject<DevJoke>(await (await client.GetAsync($"{baseApiUrl}/random").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));
            var randomJoke2 = JsonConvert.DeserializeObject<DevJoke>(await (await client.GetAsync($"{baseApiUrl}/random").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));
            var randomJoke3 = JsonConvert.DeserializeObject<DevJoke>(await (await client.GetAsync($"{baseApiUrl}/random").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));

            // Assert
            Assert.IsNotNull(randomJoke1);
            Assert.IsNotNull(randomJoke2);
            Assert.IsNotNull(randomJoke3);

            IEnumerable<int> randomJokes = new List<int>() { randomJoke1.Id };
            randomJokes = randomJokes.Union(new List<int>() { randomJoke2.Id });
            randomJokes = randomJokes.Union(new List<int>() { randomJoke3.Id });
            Assert.IsTrue(randomJokes.Count() > 1);
        }

        [TestMethod]
        public async Task DevFun_CreateJoke_OperationsSuccessful()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            var initialJokes = await GetJokes(client).ConfigureAwait(false);

            // Act
            var newJoke = CreateJoke(initialJokes.First().CategoryId);

            try
            {
                var response = await client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject(newJoke), Encoding.UTF8, "application/json-patch+json")).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                newJoke = JsonConvert.DeserializeObject<DevJoke>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                // Assert
                var receivedJokes = await GetJokes(client).ConfigureAwait(false);
                Assert.IsNotNull(receivedJokes);
                Assert.AreEqual(initialJokes.Count() + 1, receivedJokes.Count());

                var receivedNewJoke = JsonConvert.DeserializeObject<DevJoke>(await (await client.GetAsync($"{baseApiUrl}/{newJoke.Id}").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));
                Assert.AreEqual(newJoke.Id, receivedNewJoke.Id);
                Assert.AreEqual(newJoke.Author, receivedNewJoke.Author);
                Assert.AreEqual(newJoke.ImageUrl, receivedNewJoke.ImageUrl);
                Assert.AreEqual(newJoke.Text, receivedNewJoke.Text);
                Assert.AreEqual(newJoke.LikeCount, receivedNewJoke.LikeCount);
                Assert.AreEqual(newJoke.CategoryId, receivedNewJoke.CategoryId);
                Assert.AreEqual(newJoke.CategoryName, receivedNewJoke.CategoryName);
            }
            finally
            {
                var response = await client.DeleteAsync($"{baseApiUrl}/{newJoke.Id}").ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }

        [TestMethod]
        public async Task DevFun_UpdateJoke_OperationsSuccessful()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            var initialJokes = await GetJokes(client).ConfigureAwait(false);

            var newJoke = CreateJoke(initialJokes.First().CategoryId);

            try
            {
                var response = await client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject(newJoke), Encoding.UTF8, "application/json-patch+json")).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                newJoke = JsonConvert.DeserializeObject<DevJoke>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                // Act
                newJoke.Author = "Funny-Guy";
                newJoke.LikeCount = 123;

                response = await client.PutAsync($"{baseApiUrl}/{newJoke.Id}", new StringContent(JsonConvert.SerializeObject(newJoke), Encoding.UTF8, "application/json-patch+json")).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                // Assert
                var receivedJokes = await GetJokes(client).ConfigureAwait(false);
                Assert.IsNotNull(receivedJokes);
                Assert.AreEqual(initialJokes.Count() + 1, receivedJokes.Count());

                var receivedNewJoke = JsonConvert.DeserializeObject<DevJoke>(await (await client.GetAsync($"{baseApiUrl}/{newJoke.Id}").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));
                Assert.AreEqual(newJoke.Id, receivedNewJoke.Id);
                Assert.AreEqual(newJoke.Author, receivedNewJoke.Author);
                Assert.AreEqual(newJoke.ImageUrl, receivedNewJoke.ImageUrl);
                Assert.AreEqual(newJoke.Text, receivedNewJoke.Text);
                Assert.AreEqual(newJoke.LikeCount, receivedNewJoke.LikeCount);
                Assert.AreEqual(newJoke.CategoryId, receivedNewJoke.CategoryId);
                Assert.AreEqual(newJoke.CategoryName, receivedNewJoke.CategoryName);
            }
            finally
            {
                var response = await client.DeleteAsync($"{baseApiUrl}/{newJoke.Id}").ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }

        [TestMethod]
        public async Task DevFun_DeleteJoke_OperationsSuccessful()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            var initialJokes = await GetJokes(client).ConfigureAwait(false);

            var newJoke = CreateJoke(initialJokes.First().CategoryId);

            var response = await client.PostAsync(baseApiUrl, new StringContent(JsonConvert.SerializeObject(newJoke), Encoding.UTF8, "application/json-patch+json")).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            newJoke = JsonConvert.DeserializeObject<DevJoke>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            // Act
            response = await client.DeleteAsync($"{baseApiUrl}/{newJoke.Id}").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }

            newJoke = JsonConvert.DeserializeObject<DevJoke>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            // Assert
            var receivedJokes = await GetJokes(client).ConfigureAwait(false);
            Assert.IsNotNull(receivedJokes);
            Assert.AreEqual(initialJokes.Count(), receivedJokes.Count());
        }

        private DevJoke CreateJoke(int categoryId, string text = null)
        {
            var newJoke = new DevJoke()
            {
                Text = text ?? "Documentation is like sex.\nWhen it's good, it's very good.\nWhen it's bad, it's better than nothing.",
                CategoryId = categoryId
            };

            return newJoke;
        }

        private async Task<IEnumerable<DevJoke>> GetJokes(HttpClient client)
        {
            var initialJokes = JsonConvert.DeserializeObject<IEnumerable<DevJoke>>(await (await client.GetAsync($"{baseApiUrl}/?$top=100").ConfigureAwait(false)).Content.ReadAsStringAsync().ConfigureAwait(false));
            return initialJokes;
        }
    }
}