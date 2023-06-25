using Azure.Storage.Blobs.Models;
using MyInventory.Api.Models;

namespace MyInventory.Api.Services;

public interface IUploadImageService
{
    Task<UploadImageResult> UploadImage(string name, IFormFile image, Dictionary<string, string>? customMetadata = null);
}
