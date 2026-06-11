using System.Text.Json.Serialization;

namespace TrialEngine.VN;

public sealed class DialogLine
{
    [JsonPropertyName("speakerId")]
    public string SpeakerId { get; set; } = string.Empty;

    [JsonPropertyName("pose")]
    public string Pose { get; set; } = "neutral";

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
