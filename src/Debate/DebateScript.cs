using System.Text.Json.Serialization;

namespace TrialEngine.Debate;

public sealed class DebateScript
{
    [JsonPropertyName("sceneId")]
    public string SceneId { get; set; } = string.Empty;

    [JsonPropertyName("leadCharacterId")]
    public string LeadCharacterId { get; set; } = "makoto";

    [JsonPropertyName("opponentIds")]
    public List<string> OpponentIds { get; set; } = new();

    [JsonPropertyName("truthBullets")]
    public List<TruthBullet> TruthBullets { get; set; } = new();

    [JsonPropertyName("statements")]
    public List<DebateStatement> Statements { get; set; } = new();
}
