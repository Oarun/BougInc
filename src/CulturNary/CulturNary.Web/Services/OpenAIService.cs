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
    public interface IOpenAIService
    {
        Task<string> ImageRecognitionAsync(string imagePath, string requestText);
        Task<string> TextAIAsync(string dietaryRestrictions, string requestLink);
    }


    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ImageRecognitionAsync(string imagePath, string requestText)
        {
            try{
                string apiKey = _configuration["OpenAI:ImageRecognitionAppKey"];
                string base64_image = imagePath;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string textPrompt;

                if(requestText == "default")
                {
                    textPrompt = "What kind of food or ingredients is in this picture? Include \\n for any new lines and do not include any ** or bolding, but do not mention it in the text.";
                }
                else
                {
                    textPrompt = $"What would the price be to purchase the ingredients in this picture, or the ingredients to make the food in this picture, in US dollars for a person living in the Zip Code: {requestText}? Include \\n for any new lines and do not include any ** or bolding, but do not mention it in the text.";
                }

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
                                            text = textPrompt
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

                var jsonPayload = JsonConvert.SerializeObject(payload);

                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Image Recognition API call successful");
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    //Console.WriteLine("Image Recognition API call failed");
                    Console.WriteLine(response.ToString());
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    throw new HttpRequestException($"Error fetching image recognition result: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Image Recognition: " + ex.Message);
                throw new HttpRequestException($"Error fetching image recognition result: {ex.Message}");
            }
        }

        public async Task<string> TextAIAsync(string dietaryRestrictions, string requestLink)
        {
            Console.WriteLine("Dietary Restrictions: " + dietaryRestrictions);
            try{
                string apiKey = _configuration["OpenAI:ImageRecognitionAppKey"];

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                            var payload = new
                        {
                            model = "gpt-4o",
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
                                            text = "Does the recipe in this link have ingredients that conflict with any of the following dietary restrictions:" + dietaryRestrictions + ", " + requestLink + ". If so, give an explaination of what ingredients conflict or could possibly conflict, but do not include dietary restrictions that don't conflict. Include \\n for any new lines and do not include any ** or bolding, but do not mention it in the text."
                                        },
                                    }
                                }
                            },
                            max_tokens = 800
                        };

                var jsonPayload = JsonConvert.SerializeObject(payload);

                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Image Recognition API call successful");
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    //Console.WriteLine("Image Recognition API call failed");
                    Console.WriteLine(response.ToString());
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    throw new HttpRequestException($"Error fetching image recognition result: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Image Recognition: " + ex.Message);
                throw new HttpRequestException($"Error fetching image recognition result: {ex.Message}");
            }


        }
    }

}