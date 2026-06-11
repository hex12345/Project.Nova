using System.Text.Json.Serialization;

namespace TrialEngine.Debate;

public sealed class TruthBullet
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("lawDescription")]
    public string LawDescription { get; set; } = string.Empty;
}
