using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Azure.Storage.Blobs;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.Web.Services
{
    public class AzureStorageConfig
    {
        public string ConnectionString { get; set; }
        public string ImageContainerName { get; set; }
    }

    public class ImageStorageService
    {
        private readonly AzureStorageConfig _config;

        public ImageStorageService(IOptions<AzureStorageConfig> config)
        {
            _config = config.Value;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_config.ConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_config.ImageContainerName);
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);

            await blobClient.UploadAsync(file.OpenReadStream(), true);
            return blobClient.Uri.ToString();
        }
    }

}
