using Microsoft.ML.Data;

namespace MediaSuggesterAPIv2.Domain.Entities
{
    public class OnnxInput
    {
        [ColumnName("review_text")]
        public string Text { get; set; }
    }
}
