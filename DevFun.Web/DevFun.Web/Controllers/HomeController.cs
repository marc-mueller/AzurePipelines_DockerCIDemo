using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DevFun.Web.Entities;
using DevFun.Web.Options;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevFun.Web.Controllers
{
    public class HomeController : Controller
    {
        private DevFunOptions apiOptions;

        public HomeController(DevFunOptions apiOptions)
        {
            this.apiOptions = apiOptions;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["FlagEnableAlternateUrl"] = apiOptions.FlagEnableAlternateUrl;
            ViewData["UseAlternateUrl"] = Request.Query.ContainsKey("useAlternateUrl") && bool.Parse(Request.Query["useAlternateUrl"]) ? true : false;
            var joke = await GetRandomJoke();
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

        private HttpClient httpClient;

        public HttpClient Client
        {
            get
            {
                if (httpClient == null)
                {
                    string baseUrl = apiOptions.FlagEnableAlternateUrl && Request.Query.ContainsKey("useAlternateUrl") && bool.Parse(Request.Query["useAlternateUrl"]) ? apiOptions.AlternateTestingUrl : apiOptions.ApiUrl;

                    HttpClient client = new HttpClient()
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

        private async Task<DevJoke> GetRandomJoke()
        {
            DevJoke devJoke = null;
            try
            {
                HttpResponseMessage response = await Client.GetAsync("/api/jokes/random");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    if (jsonResponse != null)
                    {
                        devJoke = JsonConvert.DeserializeObject<DevJoke>(jsonResponse);
                    }
                }
            }
            catch (Exception)
            {
                // todo
            }
            return devJoke;
        }
    }
}