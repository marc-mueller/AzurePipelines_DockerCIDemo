using DevFun.Api.System.Tests.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevFun.Api.System.Tests
{
    [TestClass]
    public class JokesApi : TestBase
    {
        [TestMethod]
        public async Task AddJokes()
        {
            // Arrange 
            var client = this.CreateHttpClient();

            var initialJokes = JsonConvert.DeserializeObject<IEnumerable<DevJoke>>(await (await client.GetAsync("api/jokes")).Content.ReadAsStringAsync());

            var jokes = new List<DevJoke>() {
                new DevJoke() { Text = @"Programmer\r\nA machine that turns coffee into code." },
                new DevJoke() { Text = @"Programmer\r\nA person who fixed a problem that you don't know your have, in a way you don't understand." },
                new DevJoke() { Text = @"Algorithm\r\nWord used by programmers when... they do not want to explain what they did." },
                new DevJoke() { Text = @"Q: What's the object-oriented way to become wealthy?\r\nA: Inheritance" },
                new DevJoke() { Text = @"Q: What's the programmer's favourite hangout place?\r\nA: Foo Bar" },
                new DevJoke() { Text = @"Q: How to you tell an introverted computer scientist from an extroverted computer scientist?\r\nA: An extroverted computer scientist looks at your shoes when he talks to you." },
                new DevJoke() { Text = @"Q: Why do Java programmers wear glasses?\r\nA: Because they don't C#" },
                new DevJoke() { Text = @"A programmer had a problem. He decided to use Java.\r\nHe now has a ProblemFactory." },
            };

            // Act
            foreach (var joke in jokes)
            {
                var response = await client.PostAsync("/api/jokes", new StringContent(JsonConvert.SerializeObject(joke), Encoding.UTF8, "application/json-patch+json"));
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }

            // Assert
            var receivedJokes = JsonConvert.DeserializeObject<IEnumerable<DevJoke>>(await (await client.GetAsync("api/jokes")).Content.ReadAsStringAsync());
            Assert.AreEqual(jokes.Count - initialJokes?.Count(), receivedJokes?.Count());
        }
    }
}
