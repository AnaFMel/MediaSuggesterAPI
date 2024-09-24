using Google.Cloud.Firestore;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    [FirestoreData]
    public class GenericSuggestionList
    {
        [FirestoreProperty("filmes")]
        public List<Dictionary<string, List<GenericSuggestedMedia>>> Filmes { get; set; }

        [FirestoreProperty("series")]
        public List<Dictionary<string, List<GenericSuggestedMedia>>> Series { get; set; }
    }
}
