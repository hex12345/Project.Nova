using Godot;

namespace TrialEngine.Assets;

public partial class AudioManager : Node
{
    public IAssetLoader? AssetLoader { get; set; }

    private readonly Dictionary<string, AudioStreamPlayer> _players = new();

    public void PlaySfx(string id, float volumeDb = 0f)
    {
        if (AssetLoader == null)
            return;

        var stream = AssetLoader.GetSfx(id);
        if (stream == null)
            return;

        var player = GetOrCreatePlayer(id);
        player.Stream = stream;
        player.VolumeDb = volumeDb;
        player.Play();
    }

    private AudioStreamPlayer GetOrCreatePlayer(string id)
    {
        if (_players.TryGetValue(id, out var existing))
            return existing;

        var player = new AudioStreamPlayer { Name = $"SFX_{id}" };
        AddChild(player);
        _players[id] = player;
        return player;
    }
}
