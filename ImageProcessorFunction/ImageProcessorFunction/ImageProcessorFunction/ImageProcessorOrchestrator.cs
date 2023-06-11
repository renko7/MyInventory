using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.DurableTask;
using System.IO;
using Azure.Storage.Blobs;
using ImageProcessorFunction.Models;
using System.Text.Json;
using Azure.Storage.Blobs.Models;

namespace ImageProcessorFunction;
class ImageProcessorOrchestrator
{
    [Function(nameof(StartImageProcessing))]
    public async Task StartImageProcessing(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var req = context.GetInput<HttpRequestData>();

        var imageData = await context.CallActivityAsync<object>(nameof(UploadImageToBlobStorage), req);
    }

    [Function(nameof(UploadImageToBlobStorage))]
    public async Task<object> UploadImageToBlobStorage([ActivityTrigger] HttpRequestData req)
    {
        var requestBodyString = await new StreamReader(req.Body).ReadToEndAsync();

        var requestData = JsonSerializer.Deserialize<ImageProcessorRequest>(requestBodyString);


        BlobServiceClient blobServiceClient = new BlobServiceClient(appSettings.StorageAccountConnectionString);

        BlobContainerClient imagesContainerClient = blobServiceClient.GetBlobContainerClient("images");

        BlobClient imageClient = imagesContainerClient.GetBlobClient(picture.FileName);

        memoryStream.Position = 0;

        await imageClient.UploadAsync(requestData.ImageBase64, new BlobHttpHeaders { ContentType = "images/jpeg" });
    }


    [Function(nameof(SendImageProcessingRequest))]
    public async Task<HttpResponseData> SendImageProcessingRequest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        [DurableClient] DurableTaskClient client,
        FunctionContext executionContext)
    {
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(StartImageProcessing), req);

        return client.CreateCheckStatusResponse(req, instanceId);
    }
}
