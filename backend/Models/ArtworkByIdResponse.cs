using System.Text.Json.Serialization;
using backend.Models;

public class ArtworkByIdResponse
{
    [JsonPropertyName("data")]
    public ArtworkData Data { get; set; }
}
