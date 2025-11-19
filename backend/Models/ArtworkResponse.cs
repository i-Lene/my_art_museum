

using System.Text.Json.Serialization;

namespace backend.Models
{
    public class ArtworkResponse
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }

        [JsonPropertyName("data")]
        public List<ArtworkData> Data { get; set; }
    }

}

