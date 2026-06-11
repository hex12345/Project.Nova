using System.Text.Json.Serialization;

namespace TrialEngine.Debate;

public sealed class DebateStatement
{
    [JsonPropertyName("speakerId")]
    public string SpeakerId { get; set; } = string.Empty;

    [JsonPropertyName("rawText")]
    public string RawText { get; set; } = string.Empty;

    [JsonPropertyName("weakPoints")]
    public List<WeakPoint> WeakPoints { get; set; } = new();

    [JsonPropertyName("speed")]
    public float Speed { get; set; } = 200f;

    [JsonPropertyName("jitterStrength")]
    public float JitterStrength { get; set; } = 4f;

    [JsonPropertyName("rotationStrength")]
    public float RotationStrength { get; set; } = 8f;
}
