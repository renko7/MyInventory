using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.DurableTask;
using System.IO;
using Azure.Storage.Blobs;
using ImageProcessorFunction.Models;
using System.Text.Json;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace ImageProcessorFunction;
class ImageProcessorOrchestrator
{
    private readonly AppSettings _appSettings;
    public ImageProcessorOrchestrator(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    [Function(nameof(StartImageProcessing))]
    public async Task StartImageProcessing(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var req = context.GetInput<ImageProcessorRequest>();

        var blobUploadInformation = await context.CallActivityAsync<BlobContentInfo>(nameof(UploadImageToBlobStorage), req);
        await context.CallActivityAsync(nameof(UpdateImagesDatabase));
    }

    [Function(nameof(UpdateImagesDatabase))]
    public async Task UpdateImagesDatabase([ActivityTrigger] BlobContentInfo blobUploadInformation)
    {

    }

    [Function(nameof(UploadImageToBlobStorage))]
    public async Task<BlobContentInfo> UploadImageToBlobStorage([ActivityTrigger] ImageProcessorRequest imageProcessorRequest)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageAccountConnectionString);

        BlobContainerClient imagesContainerClient = blobServiceClient.GetBlobContainerClient("images");

        BlobClient imageClient = imagesContainerClient.GetBlobClient(imageProcessorRequest.ImageName);

        using MemoryStream imageStream = new MemoryStream(Convert.FromBase64String(imageProcessorRequest.ImageBase64));

        var blobUploadInformation = await imageClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = imageProcessorRequest.ImageType });

        return blobUploadInformation;
    }


    [Function(nameof(SendImageProcessingRequest))]
    public async Task<HttpResponseData> SendImageProcessingRequest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        var requestBodyString = await new StreamReader(req.Body).ReadToEndAsync();

        var requestData = JsonSerializer.Deserialize<ImageProcessorRequest>(requestBodyString);

        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(StartImageProcessing), input: requestData);

        return client.CreateCheckStatusResponse(req, instanceId);
    }
}
