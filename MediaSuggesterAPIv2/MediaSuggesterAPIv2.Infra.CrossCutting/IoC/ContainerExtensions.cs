using Google.Cloud.Firestore;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Domain.Services;
using MediaSuggesterAPIv2.Infra.Data.Helpers;
using MediaSuggesterAPIv2.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;

namespace MediaSuggesterAPIv2.Infra.CrossCutting.IoC
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string credentialPath = "firebase-auth.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

            FirestoreDb db = FirestoreDb.Create("projetopi5");

            services.AddSingleton(db);

            services.AddTransient<ISuggestionRepository, SuggestionRepository>();

            services.AddTransient<SuggestionService>();
            services.AddTransient<TMDBClient>();
            services.AddTransient<MLContext>();

            return services;
        }
    }
}
