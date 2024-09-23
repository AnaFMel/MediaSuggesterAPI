using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;
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
            ////consumo da IA, mandando o ReviewText pra analizar e adotando o comportamento necessário para cada resultado.
                ///
                //colocar no proj do domain dps
                //< ItemGroup >

                //    < None Include = "bert-model.onnx" >

                //        < CopyToOutputDirectory > PreserveNewest </ CopyToOutputDirectory >

                //    </ None >

                //</ ItemGroup >

                //var inputColumns = new string[] { "review_text" };

                //var outputColumns = new string[] { "predition" };

                //var predicPipeline =
                //            _mlContext
                //                .Transforms
                //                .ApplyOnnxModel(
                //                    outputColumnNames: outputColumns,
                //                    inputColumnNames: inputColumns,
                //                    ONNX_MODEL_PATH);

                //var emptyDv = _mlContext.Data.LoadFromEnumerable(new OnnxInput[] { });

                //var onnxPredictionPipeline = predicPipeline.Fit(emptyDv);

                //var onnxPredictionEngine = _mlContext.Model.CreatePredictionEngine<OnnxInput, OnnxOutput>(onnxPredictionPipeline);

                //var testInput = new OnnxInput
                //{
                //    Text = review.ReviewText
                //};

                //var prediction = onnxPredictionEngine.Predict(testInput);

                //if (prediction.Predito.Equals(nameof(Predictions.positivo)))
                //{
                //se for predito que é um comentário positivo, criamos uma sugestão personalizada, que vai ser exibida no app como um novo carrosel
                //var teste = _client.GetRecommendationsBasedOnMedia(review.MediaId, review.MediaType);
            //}
            //else
            //{
            //    //se for predito que o comentário é negativo, pegamos as recomendações originais e tiramos filmes parecidos, substituindo por outros
                var sugestoes_atuais = _suggestionRepository.GetSuggestions(review.UserId);
            //}
        }
    }
}
