using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public abstract class GenericMedia
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public string Titulo { get; set; }

        [JsonProperty("overview")]
        public string Sinopse { get; set; }

        [JsonProperty("media_type")]
        public string TipoMidia { get; set; }

        [JsonProperty("genre_ids")]
        public int[] GenerosId { get; set; }

        [JsonProperty("popularity")]
        public double Popularidade { get; set; }

        [JsonProperty("vote_average")]
        public double NotaMedia { get; set; }

        [JsonProperty("vote_count")]
        public int QtdeVotos { get; set; }
    }
}