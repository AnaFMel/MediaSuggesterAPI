namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class Review
    {
        public string UserId { get; private set; }
        public int MediaId { get; private set; }
        public string MediaType { get; private set; }
        public string ReviewText { get; private set; }
    }
}
