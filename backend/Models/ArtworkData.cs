using System.Text.Json.Serialization;

namespace backend.Models
{
    public class ArtworkData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("artist_display")]
        public string Artist_Display { get; set; }

        [JsonPropertyName("credit_line")]
        public string Credit_Line { get; set; }

        [JsonPropertyName("place_of_origin")]
        public string Origin { get; set; }

        [JsonPropertyName("date_display")]
        public string Date_Display { get; set; }

        [JsonPropertyName("artist_titles")]
        public List<string> Artist_Titles { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonPropertyName("image_id")]
        public string Image_Id { get; set; }

        public string Full_Img_Url =>
            !string.IsNullOrWhiteSpace(Image_Id) && Config != null
                ? $"{Config.iiif_url}/{Image_Id}/full/max/0/default.jpg"
                : "https://placehold.co/600x400?text=No+Image+Available";


        public string Alt_Text =>
            Thumbnail?.Alt_Text ?? "No description available";

        [JsonIgnore]
        public Config Config { get; set; }
    }
}
