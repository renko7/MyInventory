namespace MyInventory.Api.Models;

public record UploadImageCommand(Guid ItemPublicId, IFormFile Picture);

