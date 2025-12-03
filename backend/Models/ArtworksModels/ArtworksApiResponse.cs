

using System.Text.Json.Serialization;

namespace backend.Models
{
    public class ArtworksApiResponse
    {
        [JsonPropertyName("pagination")]
        public Pagination? Pagination { get; set; }

        [JsonPropertyName("data")]
        public List<ArtworkData>? Data { get; set; }

        [JsonPropertyName("config")]
        public Config? Config { get; set; }
    }

}

