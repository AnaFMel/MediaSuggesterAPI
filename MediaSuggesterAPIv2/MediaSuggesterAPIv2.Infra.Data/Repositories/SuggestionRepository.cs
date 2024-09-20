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

        public async Task<SuggestionList> GetSuggestions(string reviewId)
        {
            try
            {
                DocumentReference docRef = _firestore.Collection("suggestions").Document(reviewId);

                DocumentSnapshot document = await docRef.GetSnapshotAsync();

                if (!document.Exists) return new SuggestionList();

                SuggestionList suggestion = document.ConvertTo<SuggestionList>();

                Dictionary<string, object> teste = document.ToDictionary();

                return new SuggestionList
                {
                    Id = document.Id,
                    //Filmes = filmes,
                    //Series = series
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
}
