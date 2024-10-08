using DotNetEnv;
using MediaSuggesterAPIv2.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

string url = Environment.GetEnvironmentVariable("TMDBApiOptions_Url")!;
string key = Environment.GetEnvironmentVariable("TMDBApiOptions_Key")!;

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>{
    { "TMDB:Url", url },
    { "TMDB:Key", key }
});

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseAuthorization();

app.MapControllers();

app.Run();
