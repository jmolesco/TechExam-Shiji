using ShijiGroup.Infrastracture.Interface;
using ShijiGroup.Infrastracture.Service;
using ShijiGroup.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IWordFinderService, WordFinderService>();

builder.Services.AddDbContext<WordFinderContext>(options => options.UseInMemoryDatabase(databaseName: "WordFinderDB"));
builder.Services.AddScoped<DataSeeder>(); //seed the data
var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DataSeeder>();
context.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

