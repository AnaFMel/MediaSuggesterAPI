using Microsoft.ML.Data;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class OnnxModelOutput
    {
        [ColumnName("prediction")]
        public string Predito { get; set; }
    }
}
