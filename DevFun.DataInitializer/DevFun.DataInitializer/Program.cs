using System;
using McMaster.Extensions.CommandLineUtils;

namespace DevFun.DataInitializer
{
    public class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "Sets the base URL of the greeter service. Default is http://localhost:4567.", LongName = "service", ShortName = "s")]
        public string ServiceBaseUrl { get; } = "http://localhost:4567";

        private void OnExecute()
        {
            var devFunService = new DevFunService(new Uri(this.ServiceBaseUrl));
            var dataInitializer = new DataInitializer(devFunService);
            dataInitializer.InitializeData().Wait();
            Console.WriteLine("Data initialized.");
        }
    }
}