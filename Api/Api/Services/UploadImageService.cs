using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using MyInventory.Api.Models;

namespace MyInventory.Api.Services;

public class UploadImageService : IUploadImageService
{
    private readonly AppSettings _appSettings;

    public UploadImageService(IOptions<AppSettings> options)
    {
      _appSettings = options.Value;
    }

    public async Task<UploadImageResult> UploadImage(string name, IFormFile image, Dictionary<string, string>? customMetadata = null)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageAccountConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_appSettings.StorageAccountContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(name);

        var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        memoryStream.Position = 0;


        var uploadImageResult = await blobClient.UploadAsync(memoryStream);
        var uploadMetaDataResult = await blobClient.SetMetadataAsync(customMetadata);

        var result = new UploadImageResult(
                !uploadImageResult.GetRawResponse().IsError,
                !uploadMetaDataResult.GetRawResponse().IsError);

        return result;
    }
}
