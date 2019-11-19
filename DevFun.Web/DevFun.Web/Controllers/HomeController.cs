using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using DevFun.Web.Dtos;
using DevFun.Web.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DevFun.Web.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "ok for sample")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "ok for sample")]
    public class HomeController : Controller
    {
        private readonly DevFunOptions apiOptions;
        private readonly ILogger<HomeController> logger;
        private HttpClientHandler clientHandler;
        private HttpClient httpClient;

        public HomeController(DevFunOptions apiOptions, ILogger<HomeController> logger)
        {
            this.apiOptions = apiOptions ?? throw new ArgumentNullException(nameof(apiOptions));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            ViewData["FlagEnableAlternateUrl"] = apiOptions.FlagEnableAlternateUrl;
            ViewData["UseAlternateUrl"] = Request.Query.ContainsKey("useAlternateUrl") && bool.Parse(Request.Query["useAlternateUrl"]) ? true : false;
            var joke = await GetRandomJoke().ConfigureAwait(false);
            if (joke != null && joke.CategoryName == null)
            {
                joke.CategoryName = (await GetCategory(joke.CategoryId).ConfigureAwait(false))?.Name;
            }

            return View(joke);
        }

        public IActionResult About()
        {
            ViewData["DeploymentEnvironment"] = apiOptions.DeploymentEnvironment;

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            this.httpClient?.Dispose();
            this.clientHandler?.Dispose();

            base.Dispose(disposing);
        }

        private HttpClient Client
        {
            get
            {
                if (httpClient == null)
                {
                    string baseUrl = apiOptions.FlagEnableAlternateUrl && Request.Query.ContainsKey("useAlternateUrl") && bool.Parse(Request.Query["useAlternateUrl"]) ? apiOptions.AlternateTestingUrl : apiOptions.ApiUrl;

                    clientHandler = CreateClientHandler(apiOptions.FlagEnableAlternateUrl);
                    HttpClient client = new HttpClient(clientHandler)
                    {
                        BaseAddress = new Uri(baseUrl)
                    };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient = client;
                }
                return httpClient;
            }
        }

        private async Task<DevJokeDto> GetRandomJoke()
        {
            DevJokeDto devJoke = null;
            try
            {
                HttpResponseMessage response = await Client.GetAsync("/api/v1.0/jokes/random").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (jsonResponse != null)
                    {
                        devJoke = JsonConvert.DeserializeObject<DevJokeDto>(jsonResponse);
                    }
                }
                else
                {
                    this.logger.LogWarning($"Could not read category: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occurred while get random joke: {ex.Message}");
            }
            return devJoke;
        }

        private async Task<CategoryDto> GetCategory(int id)
        {
            CategoryDto category = null;
            try
            {
                HttpResponseMessage response = await Client.GetAsync("/api/v1.0/category/" + id.ToString(CultureInfo.InvariantCulture)).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (jsonResponse != null)
                    {
                        category = JsonConvert.DeserializeObject<CategoryDto>(jsonResponse);
                    }
                }
                else
                {
                    this.logger.LogWarning($"Could not read category: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occurred while get category: {ex.Message}");
            }
            return category;
        }

        private HttpClientHandler CreateClientHandler(bool acceptAnyServerCertificates)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (acceptAnyServerCertificates)
                {
                    this.logger.LogWarning($"Accepting invalid SSL Certificate ({sslPolicyErrors.ToString()})");
                    return true;
                }

                return sslPolicyErrors == SslPolicyErrors.None;
            };

            return clientHandler;
        }
    }
}