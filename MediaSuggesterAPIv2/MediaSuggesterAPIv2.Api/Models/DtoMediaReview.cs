namespace MediaSuggesterAPIv2.Api.Models
{
    public class DtoMediaReview
    {
        public string UserId { get; set; }
        public int MediaId { get; set; }
        public string MediaType { get; set; }
        public string ReviewText { get; set; }
    }
}