using System;
using System.Reflection;
using _4tecture.AspNetCoreExtensions.Controllers;
using DevFun.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFun.Api.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StatusController : FrameworkControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public StatusController(IWebHostEnvironment environment)
        {
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StatusResponseDto> GetStatus()
        {
            var status = new StatusResponseDto
            {
                AssemblyInfoVersion = this.GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                AssemblyVersion = this.GetType().Assembly.GetName().Version.ToString(),
                AssemblyFileVersion = this.GetType().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,

                MachineName = Environment.MachineName,
                EnvironmentName = this.environment?.EnvironmentName ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };

            return Ok(status);
        }
    }
}