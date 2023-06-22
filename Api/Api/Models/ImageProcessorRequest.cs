namespace MyInventory.Api.Models;

public record ImageProcessorRequest(Guid itemId, string base64ImageString, string imageType, string imageName);
