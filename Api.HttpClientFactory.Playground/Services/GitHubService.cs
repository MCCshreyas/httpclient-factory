using System.Net.Http;
using System.Threading.Tasks;

namespace Api.HttpClientFactory.Playground.Services
{
    public interface IGitHubService
    {
        Task<string> GetAspNetDocsIssues();
    }

    public class GitHubService : IGitHubService
    {
        private readonly HttpClient httpClient;

        public GitHubService(HttpClient httpClient)
        {
            // GitHub API versioning
            httpClient.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");

            this.httpClient = httpClient;
        }

        public async Task<string> GetAspNetDocsIssues()
        {
            using var response = await httpClient.GetAsync(
                "/repos/aspnet/AspNetCore.Docs/issues?state=open&sort=created&direction=desc");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
