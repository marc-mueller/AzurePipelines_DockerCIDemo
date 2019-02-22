using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevFun.Web.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using DevFun.Web.Entities;

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
                    HttpClient client = new HttpClient()
                    {
                        BaseAddress = new Uri(apiOptions.ApiUrl)
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
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/jokes/random");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    if (jsonResponse != null)
                    {
                        devJoke = JsonConvert.DeserializeObject<DevJoke>(jsonResponse);
                    }
                }
            }
            catch(Exception)
            {
                // todo
            }
            return devJoke;
        }
    }
}
