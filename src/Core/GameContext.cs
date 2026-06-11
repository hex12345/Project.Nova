using Godot;
using TrialEngine.Assets;

namespace TrialEngine.Core;

public sealed class GameContext
{
    public required SceneTree Tree { get; init; }
    public required Node Root { get; init; }
    public required HybridSceneDirector SceneDirector { get; init; }
    public required IAssetLoader AssetLoader { get; init; }
    public required AudioManager Audio { get; init; }
    public required TrialClock Clock { get; init; }
    public required FocusGauge Focus { get; init; }
    public required EventBus Events { get; init; }

    public Dictionary<string, object> Blackboard { get; } = new();

    public T Get<T>(string key)
    {
        if (!Blackboard.TryGetValue(key, out var value))
            throw new KeyNotFoundException($"Blackboard key missing: {key}");

        return (T)value;
    }

    public void Set<T>(string key, T value)
    {
        Blackboard[key] = value!;
    }

    public bool TryGet<T>(string key, out T? value)
    {
        if (Blackboard.TryGetValue(key, out var raw) && raw is T cast)
        {
            value = cast;
            return true;
        }

        value = default;
        return false;
    }
}
