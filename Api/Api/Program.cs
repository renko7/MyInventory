using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyInventory.Api;
using MyInventory.Api.EndpointLogic;
using MyInventory.Api.Models;


var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("Values").Get<AppSettings>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyInventoryDbContext>(
    options => 
        options.UseSqlServer(appSettings.DatabaseConnectionString));

builder.Services.AddServices(builder);

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

MyInventoryEndpoints.MapMyInventoryEndpoints(app);

app.Run();