using System.Text.Json.Serialization;

namespace TrialEngine.Assets;

public sealed class CharacterManifest
{
    [JsonPropertyName("characters")]
    public Dictionary<string, CharacterAssetEntry> Characters { get; set; } = new();
}

public sealed class AudioManifest
{
    [JsonPropertyName("sfx")]
    public Dictionary<string, string> Sfx { get; set; } = new();
}

public sealed class CharacterAssetEntry
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("sprites")]
    public Dictionary<string, string> Sprites { get; set; } = new();
}
