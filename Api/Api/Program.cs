using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyInventory.Api;
using MyInventory.Api.Models;

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
    MyInventoryDbContext dbContext) =>
{
    _ = await dbContext.Items.AddAsync(new Item { Name = "myName", Description = "myDescription" });

    _ = await dbContext.SaveChangesAsync();

    return Results.Ok("Item registered succesfully");
});

app.MapPost("/upload-image", async (
    IFormFile picture) =>
{
    // IFormFile -> Stream

    var memoryStream = new MemoryStream();

    await picture.CopyToAsync(memoryStream);

    var byteArray = memoryStream.ToArray();

    var base64 = Convert.ToBase64String(byteArray);

    BlobServiceClient blobServiceClient = new BlobServiceClient(appSettings.StorageAccountConnectionString);

    BlobContainerClient imagesContainerClient = blobServiceClient.GetBlobContainerClient("images");

    BlobClient imageClient = imagesContainerClient.GetBlobClient(picture.FileName);

    memoryStream.Position = 0;

    await imageClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = "images/jpeg" });
});

app.Run();