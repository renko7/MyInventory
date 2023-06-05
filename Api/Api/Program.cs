using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyInventory.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("Values"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/add-item", (
    [FromServices] IOptions<AppSettings> _appSettings) =>
{
    var appSettings = _appSettings.Value;

    return "123";
});

app.Run();