using MediaSuggesterAPI.IoC;
using MediaSuggesterAPI.Middlewares;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CustomCorsMiddleware>();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();