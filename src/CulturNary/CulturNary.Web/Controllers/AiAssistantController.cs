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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.Web.Controllers
{
    public class AiAssistantController : Controller
    {
        private readonly IOpenAIService _openAIService;
        private readonly ImageStorageService _imageStorageService;
        private readonly UserManager<SiteUser> _userManager; 

        public AiAssistantController(IOpenAIService openAIService, ImageStorageService imageStorageService, UserManager<SiteUser> userManager)
        {
            _openAIService = openAIService;
            _imageStorageService = imageStorageService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Signed,Admin")]
        public IActionResult AiAssistant()
        {
            return View();
        }

        [Authorize(Roles = "Signed,Admin")]
        [HttpPost]
        public async Task<IActionResult> AiAssistant(AiResponse model, ImageRecognitionRequest request)
        // public async Task<IActionResult> ImageRecognition(ImageRecognitionResult model, IFormFile file)
        {
            if (request.file != null && request.file.Length > 0)
            {
                try
                {
                    var imageUrl = await _imageStorageService.UploadImageAsync(request.file);
                    var prompt = request.zipCode == null || request.zipCode == "" ? "default" : request.zipCode;

                    string base64Image;
                    using (var ms = new MemoryStream())
                    {
                        await request.file.CopyToAsync(ms);
                        base64Image = Convert.ToBase64String(ms.ToArray());
                    }

                    Console.WriteLine("Calling Image Recognition API");
                    // Call the image recognition service
                    var resultJSON = await _openAIService.ImageRecognitionAsync(base64Image, prompt);
                    var result = JsonConvert.DeserializeObject<OpenAIResponse>(resultJSON);

                    AiResponse resultCompiled = new AiResponse(){
                        response = result,
                        imageUrl = imageUrl,
                        textRequest = null,
                    };  
                    
                    // Return the deserialized model?
                    return View("AiAssistant", resultCompiled);
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

        [Authorize(Roles = "Signed,Admin")]
        [HttpPost]
        public async Task<IActionResult> ImageRecognitionCam(AiResponse model, ImageRecognitionRequest request)
        // public async Task<IActionResult> ImageRecognitionCam(ImageRecognitionResult model, IFormFile file)
        {
            if (request.file != null && request.file.Length > 0)
            {
                try
                {
                    var imageUrl = await _imageStorageService.UploadImageAsync(request.file);
                    var prompt = request.zipCode == null || request.zipCode == "" ? "default" : request.zipCode;

                    string base64Image;
                    using (var ms = new MemoryStream())
                    {
                        await request.file.CopyToAsync(ms);
                        base64Image = Convert.ToBase64String(ms.ToArray());
                    }

                    Console.WriteLine("Calling Image Recognition API");
                    // Call the image recognition service
                    var resultJSON = await _openAIService.ImageRecognitionAsync(base64Image, prompt);
                    var result = JsonConvert.DeserializeObject<OpenAIResponse>(resultJSON);

                    AiResponse resultCompiled = new AiResponse(){
                        response = result,
                        imageUrl = imageUrl,
                        textRequest = null,
                    };  
                    
                    // Return the deserialized model?
                    return Ok(resultCompiled);
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

        [Authorize(Roles = "Signed,Admin")]
        [HttpPost]
        public async Task<IActionResult> AiAssistantText(AiResponse model, AiTextRequest request)
        {
            Console.WriteLine("Request: " + request.recipeURL);
            if (request.recipeURL != null && request.recipeURL != "")
            {
                try
                {
                    Console.WriteLine("Calling Text AI API");
                    // Call the text AI service

                    var user = await _userManager.GetUserAsync(User);
                    var dietaryRestrictionsList = user.GetDietaryRestrictions()
                                                    .Where(r => r.Active)
                                                    .Select(r => r.Name);

                    var dietaryRestrictions = dietaryRestrictionsList.Any() 
                        ? string.Join(", ", dietaryRestrictionsList) 
                        : "No dietary restrictions specified.";


                    var resultJSON = await _openAIService.TextAIAsync(dietaryRestrictions, request.recipeURL);
                    var result = JsonConvert.DeserializeObject<OpenAIResponse>(resultJSON);

                    AiResponse resultCompiled = new AiResponse(){
                        response = result,
                        imageUrl = null,
                        textRequest = request.recipeURL,
                    };  
                    
                    // Return the deserialized model?
                    return View("AiAssistant", resultCompiled);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing the text: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter some text.");
            }

            return View("AiAssistant", model);
        }

    }
}