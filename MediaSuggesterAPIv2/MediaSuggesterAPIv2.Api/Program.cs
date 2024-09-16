using Serilog;
using DotNetEnv;
using MediaSuggesterAPIv2.Api.Models;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

string url = Environment.GetEnvironmentVariable("TMDBApiOptions_Url")!;
string key = Environment.GetEnvironmentVariable("TMDBApiOptions_Key")!;

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>{
    { "TMDBApiOptions:Url", url },
    { "TMDBApiOptions:Key", key }
});

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromFile(modelName: "SuggesterModel", filePath: "suggestions_model.zip", watchForChanges: true);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var predictionHandler =
        async (PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, ModelInput input) =>
            await Task.FromResult(predictionEnginePool.Predict(modelName: "SuggesterModel", input));

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseAuthorization();

app.MapControllers();

app.Run();
