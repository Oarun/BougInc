using Microsoft.Extensions.Configuration;

namespace CulturNary.Web.Services
{
    public interface IGoogleMapsService
    {
        Task<string> GetApiKeyAsync();
    }

    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly IConfiguration _configuration;

        public GoogleMapsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GetApiKeyAsync()
        {
            return Task.FromResult(_configuration["GoogleMapsApiKey"]);
        }
    }
}