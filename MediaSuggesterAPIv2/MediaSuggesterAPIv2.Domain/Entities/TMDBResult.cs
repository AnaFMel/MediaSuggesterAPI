
using Newtonsoft.Json;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class TMDBResult<T> where T : class
    {
        [JsonProperty("results")]
        public IEnumerable<T> Itens { get; set; }
    }
}
