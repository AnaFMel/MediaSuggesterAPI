using Microsoft.ML.Data;

namespace MediaSuggesterModel.Model
{
    public class Review
    {
        [LoadColumn(0)]
        public float userId;
        [LoadColumn(1)]
        public float movieId;
        [LoadColumn(2)]
        public float rating;
        [LoadColumn(3)]
        public float comment;
    }
}
