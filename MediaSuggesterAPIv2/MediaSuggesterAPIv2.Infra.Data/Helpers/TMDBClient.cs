using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Tags;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Infra.Data.Helpers
{
    public class TMDBClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TMDBClient( IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration["TMDB:URL"]) };
        }

        public IEnumerable<TMDBGenericMedia> GetRecommendationsBasedOnMedia(int mediaId, string type)
        {
            var response = _httpClient.GetAsync($"{type}/{mediaId}/recommendations?" +
                $"language=pt-BR" +
                $"&page=1" +
                $"&api_key={_configuration["TMDB:KEY"]}" +
                $"&sort_by=popularity.desc" +
                $"&vote_count.gte=1000" +
            $"&vote_average.gte=7.0").Result;

            var resultado = Enumerable.Empty<TMDBGenericMedia>();

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                if (type.Equals(nameof(MediaType.tv))) resultado = JsonConvert.DeserializeObject<TMDBResult<TMDBTvShow>>(json).Itens;
                if (type.Equals(nameof(MediaType.movie))) resultado = JsonConvert.DeserializeObject<TMDBResult<TMDBMovie>>(json).Itens;
            }

            return resultado;
        }
    }
}
