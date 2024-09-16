using MediaSuggesterModel.Model;
using Microsoft.ML;
using Microsoft.ML.Trainers;

internal class Program
{
    private static void Main(string[] args)
    {
        MLContext mlContext = new MLContext();

        (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);

        ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

        EvaluateModel(mlContext, testDataView, model);

        Predict(mlContext, model);

        SaveModel(mlContext, trainingDataView.Schema, model);
    }

    public static (IDataView training, IDataView test) LoadData(MLContext mlContext)
    {
        //Aqui, pegar as reviews da API ao invés desses arquivos. Talvez tenha que tratar.
        var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-train.csv");
        var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

        IDataView trainingDataView = mlContext.Data.LoadFromTextFile<Review>(trainingDataPath, hasHeader: true, separatorChar: ',');
        IDataView testDataView = mlContext.Data.LoadFromTextFile<Review>(testDataPath, hasHeader: true, separatorChar: ',');

        return (trainingDataView, testDataView);
    }

    public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
    {

        //Essa parte inteira vai ter que rever e melhorar. Incluir o NLP.

        IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: "movieId"));

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "userIdEncoded",
            MatrixRowIndexColumnName = "movieIdEncoded",
            LabelColumnName = "rating",
            NumberOfIterations = 20,
            ApproximationRank = 100
        };

        var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

        Console.WriteLine("Treinando o modelo...");
        ITransformer model = trainerEstimator.Fit(trainingDataView);

        return model;
    }

    public static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
    {
        //Mudar isso depois que mudar a forma de construir o modelo.

        Console.WriteLine("Avaliando o desempenho do modelo...");
        var prediction = model.Transform(testDataView);

        //var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "rating", scoreColumnName: "Score");

        //Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        //Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
    }

    public static void Predict(MLContext mlContext, ITransformer model)
    {
        //Analizar, porque acho que vai ter que mudar isso tudo também.

        //// <SnippetPredictionEngine>
        //Console.WriteLine("=============== Making a prediction ===============");
        //var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        //// </SnippetPredictionEngine>

        //// Create test input & make single prediction
        //// <SnippetMakeSinglePrediction>
        //var testInput = new MovieRating { userId = 6, movieId = 10, rating = 1 };

        //var movieRatingPrediction = predictionEngine.Predict(testInput);
        //// </SnippetMakeSinglePrediction> 

        //// <SnippetPrintResults>
        //if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
        //{
        //    Console.WriteLine("Movie " + testInput.movieId + " is recommended for user " + testInput.userId);
        //}
        //else
        //{
        //    Console.WriteLine("Movie " + testInput.movieId + " is not recommended for user " + testInput.userId);
        //}
        //// </SnippetPrintResults>
    }

    public static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
    {
        //Salvando como arquivo zip para utilizarmos no outro projeto
        var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "SuggesterModel.zip");

        Console.WriteLine("Salvando o modelo...");
        mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
    }
}