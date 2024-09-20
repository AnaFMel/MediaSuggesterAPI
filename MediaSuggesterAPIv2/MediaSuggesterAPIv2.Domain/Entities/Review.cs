namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class Review
    {
        public string UserId { get; set; }
        public int MediaId { get; set; }
        public string ReviewText { get; set; }
    }
}
