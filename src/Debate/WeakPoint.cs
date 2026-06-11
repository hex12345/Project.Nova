using System.Text.Json.Serialization;

namespace TrialEngine.Debate;

public sealed class WeakPoint
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public WeakPointType Type { get; set; }

    [JsonPropertyName("requiredTruthBulletId")]
    public string RequiredTruthBulletId { get; set; } = string.Empty;

    [JsonIgnore]
    public bool IsBroken { get; private set; }

    public bool TryBreak(TruthBullet bullet)
    {
        if (IsBroken)
            return false;

        if (!string.Equals(bullet.Id, RequiredTruthBulletId, StringComparison.OrdinalIgnoreCase))
            return false;

        IsBroken = true;
        return true;
    }

    public void ForceBreak()
    {
        IsBroken = true;
    }
}
