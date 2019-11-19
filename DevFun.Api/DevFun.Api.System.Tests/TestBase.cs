using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevFun.Api.System.Tests
{
    [TestClass]
    public class TestBase
    {
        public string BaseUrl
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("SystemTests_baseUrl");
                if (string.IsNullOrWhiteSpace(url))
                {
                    url = TestContext?.Properties["baseUrl"]?.ToString();
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        throw new ArgumentNullException("Invalid base url parameter detected in the test context");
                    }
                }
                return url;
            }
        }

        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.BaseUrl);
            return client;
        }

        public TestContext TestContext { get; set; }
    }
}