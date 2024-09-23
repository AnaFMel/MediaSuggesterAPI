using Google.Cloud.Firestore;
using MediaSuggesterAPIv2.Domain.Entities;
using MediaSuggesterAPIv2.Domain.Repositories;

namespace MediaSuggesterAPIv2.Infra.Data.Repositories
{
    public class SuggestionRepository : ISuggestionRepository
    {
        private readonly FirestoreDb _firestore;

        public SuggestionRepository(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        public void AddSuggestions(SuggestionList suggestionList)
        {
            throw new NotImplementedException();
        }

        public async Task<SuggestionList> GetSuggestions(string id)
        {
            try
            {
                DocumentReference docRef = _firestore.Collection("suggestions").Document(id);

                DocumentSnapshot document = await docRef.GetSnapshotAsync();

                if (!document.Exists) return new SuggestionList();

                //SuggestionList suggestion = document.ConvertTo<SuggestionList>();

                //Dictionary<string, object> teste = document.ToDictionary();

                var filmes = document.GetValue<List<dynamic>>("filmes");
                var series = document.GetValue<List<dynamic>>("series");

                return new SuggestionList
                {
                    Id = document.Id,
                    //Filmes = document.GetValue<Dictionary<string, dynamic>>("filmes"),
                   // Series = series
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateSuggestions(SuggestionList suggestionList)
        {
            throw new NotImplementedException();
        }
    }

    public static class DocumentSnapshotExtensions
    {
        public static T GetValue<T>(this DocumentSnapshot document, string fieldName)
        {
            return document.TryGetValue(fieldName, out T value) ? value : default;
        }
    }
}
