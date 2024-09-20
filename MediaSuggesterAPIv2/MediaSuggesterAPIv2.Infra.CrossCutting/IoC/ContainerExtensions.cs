using Google.Cloud.Firestore;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Domain.Services;
using MediaSuggesterAPIv2.Infra.Data.Helpers;
using MediaSuggesterAPIv2.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaSuggesterAPIv2.Infra.CrossCutting.IoC
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            string credentialPath = "C:/Users/ana.melo/Downloads/projetopi5-firebase-adminsdk-aitcx-ecb7365a28.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

            FirestoreDb db = FirestoreDb.Create("projetopi5");

            services.AddSingleton(db);

            services.AddTransient<ISuggestionRepository, SuggestionRepository>();

            services.AddTransient<SuggestionService>();
            services.AddTransient<TMDBClient>();

            return services;
        }
    }
}
