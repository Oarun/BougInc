using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CulturNary.Web.Services;
using CulturNary.Web.Models;

namespace CulturNary.Web.Controllers
{
    public class ImageRecognitionController : Controller
    {
        private readonly IImageRecognitionService _imageRecognitionService;

        public ImageRecognitionController(IImageRecognitionService imageRecognitionService)
        {
            _imageRecognitionService = imageRecognitionService;
        }

        public IActionResult ImageRecognition()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImageRecognition(ImageRecognitionResult model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    await using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    var bytes = memoryStream.ToArray();
                    var byteString = Convert.ToBase64String(bytes);

                    Console.WriteLine(byteString.Length);
                    Console.WriteLine("Calling Image Recognition API");
                    // Call the image recognition service with the base64 string
                    var resultJson = await _imageRecognitionService.ImageRecognitionAsync(byteString);
                    
                    // Deserialize the JSON response into a model
                    var result = JsonConvert.DeserializeObject<ImageRecognitionResult>(resultJson);
                    
                    // Return the deserialized model as JSON
                    return View("ImageRecognition", result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing the image: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please select a file to upload.");
            }

            return View(model);
        }

        private string ConvertToBase64(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}