
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Config
    {
        [JsonPropertyName("iiif_url")]
        public string iiif_url { get; set; }
    }
}