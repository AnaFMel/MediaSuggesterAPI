using MediaSuggesterAPI.Middlewares;
using MediaSuggesterAPI.Options;
using Microsoft.Extensions.Configuration;

namespace MediaSuggesterAPI.IoC
{
    public static class ContainKDSerExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiOptions>(configuration.GetSection(nameof(ApiOptions)));

            services.AddTransient<SuggestionService>();

            services.AddTransient<CustomCorsMiddleware>();

            return services;
        }
    }
}
