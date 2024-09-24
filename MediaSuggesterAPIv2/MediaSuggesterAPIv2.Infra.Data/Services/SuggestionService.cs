using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Domain.Tags;
using MediaSuggesterAPIv2.Infra.Data.Helpers;
using Microsoft.ML;

namespace MediaSuggesterAPIv2.Domain.Services
{
    public class SuggestionService
    {
        static string ONNX_MODEL_PATH = "bert-model.onnx";
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly TMDBClient _client;
        private readonly MLContext _mlContext;

        public SuggestionService(ISuggestionRepository suggestionRepository, MLContext mlContext, TMDBClient client)
        {
            _suggestionRepository = suggestionRepository;
            _mlContext = mlContext;
            _client = client;
        }

        public void GetSuggestionsBasedOnReviews(Review review)
        {
            //consumo da IA, mandando o ReviewText pra analizar e adotando o comportamento necessário para cada resultado.

            //colocar no proj do domain dps o código abaixo

            //<ItemGroup>
            //    <None Include = "bert-model.onnx">
            //        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
            //    </None>
            //</ItemGroup>

            var inputColumns = new string[] { "review_text" };

            var outputColumns = new string[] { "predition" };

            var predicPipeline =
                        _mlContext
                            .Transforms
                            .ApplyOnnxModel(
                                outputColumnNames: outputColumns,
                                inputColumnNames: inputColumns,
                                ONNX_MODEL_PATH);

            var emptyDv = _mlContext.Data.LoadFromEnumerable(new OnnxModelInput[] { });

            var onnxPredictionPipeline = predicPipeline.Fit(emptyDv);

            var onnxPredictionEngine = _mlContext.Model.CreatePredictionEngine<OnnxModelInput, OnnxModelOutput>(onnxPredictionPipeline);

            var testInput = new OnnxModelInput
            {
                Text = review.ReviewText
            };

            var prediction = onnxPredictionEngine.Predict(testInput);

            //pegando mídias parecidas com a avaliada
            var tmdbRecommendations = _client.GetRecommendationsBasedOnMedia(review.MediaId, review.MediaType);

            if (prediction.Predito.Equals(nameof(Predictions.positivo)))
            {
                //se for predito que é um comentário positivo, criamos sugestões personalizadas, que vão ser exibidas no app como um novo carrosel
                _suggestionRepository.AddPersonalizedSuggestions(review.UserId, review.MediaType, review.MediaId, tmdbRecommendations.Select(r => r.Id).ToArray());
            }
            else
            {
                //se for predito que o comentário é negativo, pegamos as recomendações originais e tiramos os filmes parecidos, substituindo por outros
                var sugestoes_atuais = _suggestionRepository.GetSuggestions(review.UserId);

                if (review.MediaType.Equals(nameof(MediaType.tv)))
                {
                    var total_inicial = sugestoes_atuais.Filmes.Capacity;
                    //tem que ver se algum dos elementos do tmdbRecommendations está aqui. Se estiver, tirar. O count seria para repor com outra mídias depois
                }

                if (review.MediaType.Equals(nameof(MediaType.movie)))
                {
                    var total_inicial = sugestoes_atuais.Series.Capacity;
                    //tem que ver se algum dos elementos do tmdbRecommendations está aqui. Se estiver, tirar. O count seria para repor com outra mídias depois
                }
            }
        }
    }
}
