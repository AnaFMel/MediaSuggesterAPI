using Newtonsoft.Json;
using System.Text;
using MediaSuggesterAPI.Models;
using MediaSuggesterAPI.Tags;
using MediaSuggesterAPI.Dtos;
using MediaSuggesterAPI.Options;
using Microsoft.Extensions.Options;

namespace MediaSuggesterAPI
{
    public class SuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiOptions> _apiOptions;
        public SuggestionService(IOptions<ApiOptions> options)
        {
            _apiOptions = options;
            _httpClient = new HttpClient { BaseAddress = new Uri($"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiOptions.Value.ApiKey}") };
        }

        public ISuggestions? ObterSugestoes(DtoGetSuggestion dto)
        {
            var exemplo = string.Empty;

            if (dto.TipoMidia == nameof(TipoMidia.filmes)) exemplo = "{\"filmes\" : [{\"Drama\": [{\"Titulo\":\"Titúlo do Filme Tal\"," +
                                    "\"OndeAssistir\":\"Lugar Tal\" }, " +
                                    "{\"Titulo\":\"Titúlo do Filme Tal\",\"OndeAssistir\":\"Lugar Tal\" }, " +
                                    "{\"Suspense\": [{\"Titulo\":\"Titúlo do Filme Tal\",\"OndeAssistir\":\"Lugar Tal\" }, " +
                                    "{\"Titulo\":\"Titúlo do Filme Tal\",\"OndeAssistir\":\"Lugar Tal\" }]}], ";
            if (dto.TipoMidia == nameof(TipoMidia.series)) exemplo = "\"series\" : [{\"Drama\": [{\"Titulo\":\"Titúlo da Série Tal\"," +
                                    "\"OndeAssistir\":\"Lugar Tal\" }, " +
                                    "{\"Titulo\":\"Titúlo da Série Tal\",\"OndeAssistir\":\"Lugar Tal\" }]}, " +
                                    "{\"Suspense\": [{\"Titulo\":\"Titúlo da Série Tal\",\"OndeAssistir\":\"Lugar Tal\" }, " +
                                    "{\"Titulo\":\"Titúlo da Série Tal\",\"OndeAssistir\":\"Lugar Tal\" }]}]}";

            var requestData = new Dictionary<string, dynamic>{
            { "contents", new List<Dictionary<string, dynamic>>
                {
                    new Dictionary<string, object>
                    {
                        { "parts", new List<Dictionary<string, string>>
                            {
                                new Dictionary<string, string>
                                {
                                    { "text", $"Recomende para mim {dto.TipoMidia} dos gêneros {dto.Generos}; separadamente. " +
                                    "Recomende 20 mídias para cada um dos gêneros. Não repita nenhuma mídia em diferentes gêneros" +
                                    "Considere apenas mídias com avaliação maior que 7 no IMDB. " +
                                    "Forneça a informação de onde posso assistir as mídias recomendadas. " +
                                    "Importante: não recomende a mesma mídia mais de uma vez, mesmo que em gêneros diferentes." +
                                    "Use o formato JSON exemplificado abaixo: " +
                                     exemplo }
                                }
                            }
                        }
                    }
                }
            },
            {
                "generationConfig", new Dictionary<string, string>
                {
                    { "response_mime_type", "application/json" }
                }
            }
        };

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string requestBody = JsonConvert.SerializeObject(requestData);

            var resposta = _httpClient.PostAsync("", new StringContent(requestBody, Encoding.UTF8, "application/json")).Result;

            if (resposta.IsSuccessStatusCode)
            {
                var json = resposta.Content.ReadAsStringAsync().Result;

                var response = JsonConvert.DeserializeObject<Response>(json);

                if (response == null) return null;

                var text = response
                    .Candidates
                    .FirstOrDefault(c => c.Content != null)
                    .Content
                    .Parts.FirstOrDefault(p => !string.IsNullOrEmpty(p.Text))
                    .Text;

                ISuggestions resultado = null;

                if (dto.TipoMidia == nameof(TipoMidia.series)) resultado = JsonConvert.DeserializeObject<SuggestionsSeries>(text);

                if (dto.TipoMidia == nameof(TipoMidia.filmes)) resultado = JsonConvert.DeserializeObject<SuggestionsFilmes>(text);

                return resultado;
            }
            else
            {
                return null;
            }
        }
    }
}
