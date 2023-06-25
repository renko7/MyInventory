// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyInventory.ImagesProcessor
{
    public class ImageAddedProcessor
    {
        private readonly ILogger _logger;

        public ImageAddedProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ImageAddedProcessor>();
        }

        [Function("ImageAddedProcessor")]
        public void Run([EventGridTrigger] CloudEvent imageUploadedEvent)
        {
            var serializedInput = imageUploadedEvent.ToString();

            _logger.LogInformation(serializedInput);

            Console.WriteLine(serializedInput);
        }
    }
}
