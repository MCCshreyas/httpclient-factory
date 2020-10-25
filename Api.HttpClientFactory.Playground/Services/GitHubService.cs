using System;
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
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://api.github.com/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            _httpClient = httpClient;
        }

        public async Task<string> GetAspNetDocsIssues()
        {
            using var response = await _httpClient.GetAsync(
                "/repos/aspnet/AspNetCore.Docs/issues?state=open&sort=created&direction=desc");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
