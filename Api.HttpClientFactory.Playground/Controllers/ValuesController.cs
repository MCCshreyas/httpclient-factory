using Api.HttpClientFactory.Playground.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.HttpClientFactory.Playground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public ValuesController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(_gitHubService.GetAspNetDocsIssues());
            }

            await Task.WhenAll(tasks.ToArray());

            return Ok();
        }
    }
}
