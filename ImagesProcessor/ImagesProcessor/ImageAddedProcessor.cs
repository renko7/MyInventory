// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using System.Net.Sockets;
using Azure.Messaging;
using Azure.Storage.Blobs;
using ImageProcessorFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

            var eventDataString = $"""{imageUploadedEvent?.Data.ToString()}""";

            var dataDeserialized = JsonConvert.DeserializeAnonymousType(eventDataString, new
            {
                Url = (Uri)default
            });

            var imageProperties = new
            {
                Container = dataDeserialized.Url.Segments[1].Trim('/'),
                FileName = Path.GetFileNameWithoutExtension(dataDeserialized.Url.ToString()),
                FileType = Path.GetExtension(dataDeserialized.Url.ToString())
            };

            BlobClient blobClient = new BlobClient(_appSettings.StorageAccountConnectionString, _appSettings.StorageAccountContainerName, imageProperties.FileName);

            var response = await blobClient.GetPropertiesAsync();

            var metaData = response.Value.Metadata;

            metaData.TryGetValue("ItemId", out var itemId);

            _logger.LogInformation($"""
                EventDataString = {eventDataString}
                DataDeserialized = {dataDeserialized}
                ImageProperties = {imageProperties}
                BlobMetaData = {JsonConvert.SerializeObject(metaData)}
                """);

            _logger.LogInformation($"THE BLOBMETADATA {itemId}");
      
            _logger.LogInformation($"Exiting {nameof(ImageAddedProcessor)}");
        }

    }
}
