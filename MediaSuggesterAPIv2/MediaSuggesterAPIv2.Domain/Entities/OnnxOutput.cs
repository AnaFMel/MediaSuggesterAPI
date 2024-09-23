using Microsoft.ML.Data;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class OnnxOutput
    {
        [ColumnName("prediction")]
        public string Predito { get; set; }
    }
}
