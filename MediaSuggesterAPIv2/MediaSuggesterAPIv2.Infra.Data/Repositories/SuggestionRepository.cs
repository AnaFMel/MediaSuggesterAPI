﻿using Google.Cloud.Firestore;
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

        public async void AddPersonalizedSuggestions(string userId, string mediaType, int likedMediaId, int[] suggestionList)
        {
            CollectionReference colRef = _firestore.Collection("personalized_suggestions");
            await colRef.Document().SetAsync(new Dictionary<string, object>(){
                    { "user_id", userId },
                    { "liked_media_id", likedMediaId },
                    { "suggestions", suggestionList },
                    { "typeof_liked_media", mediaType }
                });
        }

        public GenericSuggestionList GetSuggestions(string id)
        {
            DocumentReference docRef = _firestore.Collection("suggestions").Document(id);

            DocumentSnapshot document = docRef.GetSnapshotAsync().Result;

            if (!document.Exists) return new GenericSuggestionList();

            return document.ConvertTo<GenericSuggestionList>();
        }

        public async void UpdateSuggestions(string userId, GenericSuggestionList suggestionList)
        {
            DocumentReference docRef = _firestore.Collection("suggestions").Document(userId);

            await docRef.SetAsync(suggestionList, SetOptions.MergeAll);
        }
    }
}
