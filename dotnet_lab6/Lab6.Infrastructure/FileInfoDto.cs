using System.Text.Json.Serialization;

namespace Lab6.Infrastructure;

public class FileInfoDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return $"id: {Id}, Name: {Name}";
    }
}