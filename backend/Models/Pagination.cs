

using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Pagination
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("total_pages")]
        public int Total_Pages { get; set; }

        [JsonPropertyName("current_page")]
        public int Current_Page { get; set; }

        [JsonPropertyName("next_url")]
        public string Next_Url { get; set; }
    }

}

