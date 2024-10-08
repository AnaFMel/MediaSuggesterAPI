using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;
using MediaSuggesterAPIv2.Domain.Tags;
using MediaSuggesterAPIv2.Infra.Data.Helpers;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MediaSuggesterAPIv2.Domain.Services
{
    public class SuggestionService
    {
        static string ONNX_MODEL_PATH = "suggester.onnx";
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly TMDBClient _client;
        private readonly MLContext _mlContext;
        private static Dictionary<string, long> vocab;
        private static Dictionary<string, string> specialTokens;

        public SuggestionService(ISuggestionRepository suggestionRepository, MLContext mlContext, TMDBClient client)
        {
            _suggestionRepository = suggestionRepository;
            _mlContext = mlContext;
            _client = client;
        }

        public void GetSuggestionsBasedOnReviews(Review review)
        {
            LoadTokenizer("./tokenizer_files/vocab.txt", "./tokenizer_files/special_tokens_map.json");

            var (inputIds, attentionMask) = TokenizeReview(review.ReviewText, 32);

            // Passar para o modelo ONNX
            var prediction = GetModelPrediction(inputIds, attentionMask);
            string sentiment = InterpretPrediction(prediction);
            Console.WriteLine($"Review text: {review.ReviewText}");
            Console.WriteLine($"Sentiment: {sentiment}");

            //pegando mídias parecidas com a avaliada
            var tmdbRecommendations = _client.GetRecommendationsBasedOnMedia(review.MediaId, review.MediaType);

            if (sentiment.Equals(nameof(Predictions.Positivo)))
            {
                //se for predito que é um comentário positivo, criamos sugestões personalizadas, que vão ser exibidas no app como um novo carrosel
                _suggestionRepository.AddPersonalizedSuggestions(review.UserId, review.MediaType, review.MediaId, tmdbRecommendations.Select(r => r.Id).ToArray());
            }
            else
            {
                //se for predito que o comentário é negativo, pegamos as recomendações originais e tiramos os filmes parecidos, substituindo por outros

                var sugestoes_atuais = _suggestionRepository.GetSuggestions(review.UserId);

                List<Dictionary<string, List<GenericSuggestedMedia>>> listaDeMidias = new List<Dictionary<string, List<GenericSuggestedMedia>>>();

                if (review.MediaType.Equals(nameof(MediaType.tv))) listaDeMidias = sugestoes_atuais.Series;

                if (review.MediaType.Equals(nameof(MediaType.movie))) listaDeMidias = sugestoes_atuais.Filmes;

                foreach (var midiasPorGenero in listaDeMidias)
                {
                    foreach (var midias in midiasPorGenero.Values)
                    {
                        foreach (var midia in midias)
                        {
                            if (tmdbRecommendations.Select(r => r.Id).ToList().Contains(Convert.ToInt32(midia.Id))) midias.Remove(midia);
                        }
                    }
                }

                if (review.MediaType.Equals(nameof(MediaType.tv))) sugestoes_atuais.Series = listaDeMidias;

                if (review.MediaType.Equals(nameof(MediaType.movie))) sugestoes_atuais.Filmes = listaDeMidias;

                _suggestionRepository.UpdateSuggestions(sugestoes_atuais);
            }
        }

        static void LoadTokenizer(string vocabPath, string specialTokensPath)
        {
            vocab = new Dictionary<string, long>();

            var lines = File.ReadAllLines(vocabPath);
            for (long i = 0; i < lines.Length; i++)
            {
                vocab[lines[i]] = i;
            }

            var json = File.ReadAllText(specialTokensPath);
            specialTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        static (long[], long[]) TokenizeReview(string text, int maxLength)
        {
            var tokens = Tokenize(text);
            var inputIds = new List<long> { vocab[specialTokens["cls_token"]] };

            foreach (var token in tokens)
            {
                inputIds.Add(vocab.ContainsKey(token) ? vocab[token] : vocab[specialTokens["unk_token"]]);
            }

            inputIds.Add(vocab[specialTokens["sep_token"]]);

            long[] attentionMask = new long[inputIds.Count];
            for (int i = 0; i < inputIds.Count; i++) attentionMask[i] = 1;

            while (inputIds.Count < maxLength)
            {
                inputIds.Add(vocab[specialTokens["pad_token"]]);
                attentionMask = attentionMask.Append(0).ToArray();
            }

            return (inputIds.ToArray(), attentionMask);
        }

        static List<string> Tokenize(string text)
        {
            return text.Split(' ').Select(token => token.Trim()).ToList();
        }

        static float[] GetModelPrediction(long[] inputIds, long[] attentionMask)
        {
            using var session = new InferenceSession("suggester.onnx");

            var inputIdsTensor = new DenseTensor<long>(inputIds, new[] { 1, inputIds.Length });
            var attentionMaskTensor = new DenseTensor<long>(attentionMask, new[] { 1, attentionMask.Length });

            var inputs = new[]
            {
                NamedOnnxValue.CreateFromTensor("input_ids", inputIdsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", attentionMaskTensor)
            };

            using var results = session.Run(inputs);
            var outputTensor = results.First().AsEnumerable<float>().ToArray();

            return outputTensor;
        }

        static string InterpretPrediction(float[] prediction)
        {
            int predictedClass = Array.IndexOf(prediction, prediction.Max());

            string[] classNames = { "Negativo", "Positivo" };

            return classNames[predictedClass];
        }
    }
}
