using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyInventory.Api;
using MyInventory.Api.Models;
using MyInventory.Domain;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("Values").Get<AppSettings>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("Values"));

builder.Services.AddDbContext<MyInventoryDbContext>(
    options => 
        options.UseSqlServer(appSettings.DatabaseConnectionString));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/add-item", async (
    [FromServices] IOptions<AppSettings> appSettings,
    AddItemRequest itemData,
    MyInventoryDbContext dbContext) =>
{
    _ = await dbContext.Items.AddAsync(new Item { Name = itemData.Name, Description = itemData.Description });

    _ = await dbContext.SaveChangesAsync();

    return Results.Ok("Item registered succesfully");
});

app.MapPost("{itemId}/upload-image", async (
    Guid itemId,
    IFormFile image,
    [FromServices] IHttpClientFactory httpClientFactory) =>
{
    // IFormFile -> Stream -> byte array -> base64 string

    var memoryStream = new MemoryStream();

    await image.CopyToAsync(memoryStream);

    memoryStream.Position = 0;

    var byteArray = memoryStream.ToArray();

    var base64String = Convert.ToBase64String(byteArray);

    var httpClient = httpClientFactory.CreateClient();

    var content = new StringContent(JsonSerializer.Serialize(new ImageProcessorRequest(itemId, base64String, image.ContentType, image.FileName)), Encoding.UTF8, "application/json");

    //var response = await httpClient.PostAsync($"{appSettings.ImageProcessorUrl}/api/HttpExample", content);

    var response = await httpClient.PostAsync($"{appSettings.ImageProcessorUrl}/api/SendImageProcessingRequest", content);
});

app.Run();