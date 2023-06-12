

using System.Text.Json.Serialization;

namespace ImageProcessorFunction.Models;

public class ImageProcessorRequest
{
    [JsonPropertyName("itemId")]
    public Guid ItemId { get; set; }
    [JsonPropertyName("base64ImageString")]
    public string ImageBase64 { get; set; }
    [JsonPropertyName("imageType")]
    public string ImageType { get; set; }
    [JsonPropertyName("imageName")]
    public string ImageName { get; set; }
}
