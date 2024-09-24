using Google.Cloud.Firestore;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    [FirestoreData]
    public class GenericSuggestedMedia
    {
        [FirestoreProperty("id")]
        public string Id { get; set; }

        [FirestoreProperty("ondeAssistir")]
        public string OndeAssistir { get; set; }
    }
}
