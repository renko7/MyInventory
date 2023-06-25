using Microsoft.AspNetCore.Builder;
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

        return Results.Ok(result) ;
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

        var extension = name.Split(".")[1];

        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            var randomVal = rnd.Next(33, 127);

            randomWord += (char)randomVal;
        }

        return $"{randomWord}.{extension}";
    }
}
