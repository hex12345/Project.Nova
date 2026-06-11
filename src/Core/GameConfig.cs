using System.Text.Json.Serialization;

namespace TrialEngine.Core;

public sealed class GameConfig
{
    [JsonPropertyName("globalTimerSeconds")]
    public float GlobalTimerSeconds { get; set; } = 420f;

    [JsonPropertyName("focusMax")]
    public float FocusMax { get; set; } = 100f;

    [JsonPropertyName("focusDrainPerSecond")]
    public float FocusDrainPerSecond { get; set; } = 28f;

    [JsonPropertyName("focusRegenPerSecond")]
    public float FocusRegenPerSecond { get; set; } = 12f;

    [JsonPropertyName("logicDive")]
    public LogicDiveConfig LogicDive { get; set; } = new();
}

public sealed class LogicDiveConfig
{
    [JsonPropertyName("baseSpeed")]
    public float BaseSpeed { get; set; } = 26f;

    [JsonPropertyName("laneDistance")]
    public float LaneDistance { get; set; } = 4f;

    [JsonPropertyName("rewindCaptureIntervalSeconds")]
    public float RewindCaptureIntervalSeconds { get; set; } = 1f;

    [JsonPropertyName("rewindHistorySeconds")]
    public float RewindHistorySeconds { get; set; } = 12f;
}
