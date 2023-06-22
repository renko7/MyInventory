using Azure.Storage.Blobs.Models;

namespace MyInventory.Api.Services;

public interface IUploadImageService
{
    // change from blobcontentinfo to upload result to not tie to azure
    Task<BlobContentInfo> UploadImage(string name, IFormFile image, Dictionary<string, string>? customMetadata = null);
}
