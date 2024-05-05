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
using System.Drawing;

namespace CulturNary.Web.Controllers
{
    public class ImageRecognitionController : Controller
    {
        private readonly IImageRecognitionService _imageRecognitionService;
        private readonly ImageStorageService _imageStorageService;

        public ImageRecognitionController(IImageRecognitionService imageRecognitionService, ImageStorageService imageStorageService)
        {
            _imageRecognitionService = imageRecognitionService;
            _imageStorageService = imageStorageService;
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
                    var imageUrl = await _imageStorageService.UploadImageAsync(file);

                    string base64Image;
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        base64Image = Convert.ToBase64String(ms.ToArray());
                    }

                    Console.WriteLine("Calling Image Recognition API");
                    // Call the image recognition service
                    var resultJSON = await _imageRecognitionService.ImageRecognitionAsync(base64Image);
                    var result = JsonConvert.DeserializeObject<OpenAIResponse>(resultJSON);

                    ImageRecognitionResult resultCompiled = new ImageRecognitionResult(){
                        response = result,
                        imageUrl = imageUrl,
                    };  
                    
                    // Return the deserialized model?
                    return View("ImageRecognition", resultCompiled);
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

    }
}