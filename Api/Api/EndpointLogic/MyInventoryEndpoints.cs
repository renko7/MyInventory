using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using MyInventory.Api.Models;
using MyInventory.Api.Services;

namespace MyInventory.Api.EndpointLogic;

public static class MyInventoryEndpoints
{
    public static void MapMyInventoryEndpoints(WebApplication app)
    {
        app.MapPost("{itemId}/upload-image", UploadImage);
        app.MapPost("/add-item", AddItem);
    }

    public static async Task<IResult> UploadImage(Guid itemId, IFormFile image, IUploadImageService uploadImageService)
    {
        var fileName = CreateRandomWord(100, image.FileName);

        var result = await uploadImageService.UploadImage(fileName, image, new Dictionary<string, string> { { "ItemId", itemId.ToString() } });

        return Results.Ok(result);
    }

    public static async Task<IResult> AddItem(IOptions<AppSettings> appSettings, AddItemRequest itemData, MyInventoryDbContext dbContext)
    {
        _ = await dbContext.Items.AddAsync(new Item { Name = itemData.Name, Description = itemData.Description });

        _ = await dbContext.SaveChangesAsync();

        return Results.Ok("Item registered succesfully");
    }

    private static string CreateRandomWord(int length, string name)
    {
        var randomWord = "";

        var unAllowedCharacters = new HashSet<char>() { '/', '\\', ':', '?', '=', '&', '#', '.'};

        var extension = Path.GetExtension(name);

        while (randomWord.Length < length)
        {
            Random rnd = new Random();
            var randomVal = rnd.Next(48, 123);

            char c = (char)randomVal;

            if (!char.IsLetterOrDigit(c))
            {
                randomWord += c;
            }
        }
        return $"{randomWord}{extension}";
    }
}
