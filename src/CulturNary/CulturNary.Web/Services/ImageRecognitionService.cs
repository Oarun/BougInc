using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace CulturNary.Web.Services
{
    public interface IImageRecognitionService
    {
        Task<string> ImageRecognitionAsync(string imagePath);
    }


    public class ImageRecognitionService : IImageRecognitionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ImageRecognitionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ImageRecognitionAsync(string imagePath)
        {
            var appKey = _configuration["FoodVisor:ImageRecognitionAppKey"];
            var baseUrl = "https://vision.foodvisor.io/api/1.0/en/analysis/";

            // byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // MultipartFormDataContent content = new MultipartFormDataContent();
            // ByteArrayContent imageContent = new ByteArrayContent(imageBytes);
            // content.Add(imageContent, "image", "image.jpg");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Api-Key", appKey);
            Console.WriteLine(_httpClient.DefaultRequestHeaders.Authorization);

            HttpResponseMessage response = await _httpClient.PostAsync(baseUrl, new StringContent("image=" + imagePath));

            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Image Recognition Result: {response.StatusCode}, {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error fetching image recognition result: {response.ReasonPhrase}");
            }
        }
    }


}
