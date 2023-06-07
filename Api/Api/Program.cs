using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyInventory.Api;
using MyInventory.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("Values"));

builder.Services.AddDbContext<MyInventoryDbContext>(
    options => 
        options.UseSqlServer("Server=tcp:myinventorydbsqlserver.database.windows.net,1433;Initial Catalog=myinventorydb;Persist Security Info=False;User ID=alok1025;Password=Shruti219;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/add-item", async (
    [FromServices] IOptions<AppSettings> _appSettings,
    MyInventoryDbContext _dbContext) =>
{
    var z = await _dbContext.Items.AddAsync(new Item { Name = "myName", Description = "myDescription" });

    _ = await _dbContext.SaveChangesAsync();

    return "123";
});

app.Run();