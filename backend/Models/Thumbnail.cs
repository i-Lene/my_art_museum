

using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Thumbnail
    {
        [JsonPropertyName("lqip")]
        public string Lqip { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("alt_text")]
        public string Alt_Text { get; set; }
    }

}



