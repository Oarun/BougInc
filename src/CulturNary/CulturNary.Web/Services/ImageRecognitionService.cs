using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Net.Http.Headers;
using SpecFlow.Internal.Json;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Text;

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
            try{
                
                Console.WriteLine("                                             SERVICE BEING CALLED");

                string apiKey = _configuration["OpenAI:ImageRecognitionAppKey"];
                Console.WriteLine("                                             API KEY BEING SET");
                string base64_image = imagePath;
                Console.WriteLine("                                             BASE64IMAGE BEING SET");

                var client = new HttpClient();
                Console.WriteLine("                                             CLIENT HAS BEEN CREATED");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                Console.WriteLine("                                             AUTHORIZATION HAS BEEN ADDED");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                Console.WriteLine("                                             CONTENT-TYPE HAS BEEN ADDED");

                Console.WriteLine("                                             HEADERS HAVE BEEN CREATED");

                var payload = new
                        {
                            model = "gpt-4-turbo",
                            messages = new[]
                            {
                                new
                                {
                                    role = "user",
                                    content = new object[]
                                    {
                                        new
                                        {
                                            type = "text",
                                            text = "What kind of food or ingredients is in this picture?"
                                        },
                                        new
                                        {
                                            type = "image_url",
                                            image_url = new
                                            {
                                                url = $"data:image/jpeg;base64,{base64_image}",
                                                detail = "low"
                                            }
                                        }
                                    }
                                }
                            },
                            max_tokens = 800
                        };

                Console.WriteLine("                                             PAYLOAD HAS BEEN CREATED");

                var jsonPayload = JsonConvert.SerializeObject(payload);

                Console.WriteLine("                                             PAYLOAD HAS BEEN CONVERTED");

                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                Console.WriteLine("                                             HTTPCONTENT HAS BEEN CREATED");

                Console.WriteLine("Initiating API Call to OpenAI Image Recognition API");

                HttpResponseMessage response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Image Recognition API call successful");
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Image Recognition API call failed");
                    Console.WriteLine(response.ToString());
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    throw new HttpRequestException($"Error fetching image recognition result: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Image Recognition Service: " + ex.Message);
                throw new HttpRequestException($"Error fetching image recognition result: {ex.Message}");
            }
        }
    }

    //     public async Task<string> ImageRecognitionAsync(string imagePath)
    //     {
    //         var appKey = _configuration["FoodVisor:ImageRecognitionAppKey"];
    //         var baseUrl = "https://vision.foodvisor.io/api/1.0/en/analysis/";

    //         // byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

    //         // MultipartFormDataContent content = new MultipartFormDataContent();
    //         // ByteArrayContent imageContent = new ByteArrayContent(imageBytes);
    //         // content.Add(imageContent, "image", "image.jpg");

    //         _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Api-Key", appKey);
    //         Console.WriteLine(_httpClient.DefaultRequestHeaders.Authorization);

    //         HttpResponseMessage response = await _httpClient.PostAsync(baseUrl, new StringContent("image=" + imagePath));

    //         string responseContent = await response.Content.ReadAsStringAsync();
    //         Console.WriteLine($"Image Recognition Result: {response.StatusCode}, {responseContent}");

    //         if (response.IsSuccessStatusCode)
    //         {
    //             return await response.Content.ReadAsStringAsync();
    //         }
    //         else
    //         {
    //             throw new HttpRequestException($"Error fetching image recognition result: {response.ReasonPhrase}");
    //         }
    //     }
    // }

}