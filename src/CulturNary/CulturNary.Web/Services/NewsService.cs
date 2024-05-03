using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CulturNary.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CulturNary.Web.Services
{
    public interface INewsService
    {
        Task<string> GetNewsAsync(string query);
    }
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Add headers as they are in Postman
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.36.3");
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        }


        public async Task<string> GetNewsAsync(string query)
        {
            var apiKey = _configuration["NewsApiKey"];
            var baseUrl = "https://newsapi.org/v2/everything?";
            var encodedQuery = HttpUtility.UrlEncode(query);
            var url = $"{baseUrl}q={encodedQuery}&apiKey={apiKey}";

            Console.WriteLine($"URL being accessed: {url}");  // Log the URL for verification

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to fetch news: {response.StatusCode}, {response.ReasonPhrase}, {errorContent}");
                throw new HttpRequestException($"Error fetching news: {response.StatusCode} {response.ReasonPhrase}");
            }
        }

    }
}
