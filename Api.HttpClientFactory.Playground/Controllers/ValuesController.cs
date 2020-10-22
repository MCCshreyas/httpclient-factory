using Api.HttpClientFactory.Playground.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.HttpClientFactory.Playground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IServiceProvider service;

        public ValuesController(IServiceProvider service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                using (var scope = service.CreateScope())
                {
                    var gitHubService = scope.ServiceProvider.GetService<IGitHubService>();
                    tasks.Add(gitHubService.GetAspNetDocsIssues());
                }
            }

            await Task.WhenAll(tasks.ToArray());

            return Ok();
        }
    }
}
