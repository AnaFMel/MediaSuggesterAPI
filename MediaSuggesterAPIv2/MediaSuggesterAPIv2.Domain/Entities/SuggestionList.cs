using Google.Cloud.Firestore;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class SuggestionList
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public MediaSuggestions Filmes { get; set; }
        [FirestoreProperty]
        public MediaSuggestions Series { get; set; }
    }
}
