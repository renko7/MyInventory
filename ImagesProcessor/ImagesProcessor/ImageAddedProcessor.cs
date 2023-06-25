// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Azure.Messaging;
using Azure.Storage.Blobs;
using ImageProcessorFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace MyInventory.ImagesProcessor
{
    public class ImageAddedProcessor
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;

        public ImageAddedProcessor(IOptions<AppSettings> appSettings, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings.Value;
            _logger = loggerFactory.CreateLogger<ImageAddedProcessor>();
        }

        [Function("ImageAddedProcessor")]
        public async Task Run([EventGridTrigger] CloudEvent imageUploadedEvent)
        {
            _logger.LogInformation($"Entering {nameof(ImageAddedProcessor)}");

            var serializedInput = new
            {
                Id = imageUploadedEvent.Id,
                Type = imageUploadedEvent.Type,
                DataFormat = imageUploadedEvent.DataContentType,
                Data = imageUploadedEvent?.Data?.ToString(),
                SpecVersion = "1.0 --> hardcoded in cloudevent class"
            }.ToString();

            _logger.LogInformation(serializedInput);

            var eventData = $"""{imageUploadedEvent?.Data}""";

            JObject eventDataJson = JObject.Parse(eventData);

            BlobClient blobClient = new BlobClient((Uri)eventDataJson["url"]);

            var response = await blobClient.GetPropertiesAsync();

            var metaData = response.Value.Metadata;

            metaData.TryGetValue("ItemId", out var itemId);

            _logger.LogInformation($"THE BLOBMETADATA {itemId}");
      
            _logger.LogInformation($"Exiting {nameof(ImageAddedProcessor)}");
        }

    }
}
