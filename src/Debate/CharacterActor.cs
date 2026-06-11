using Godot;
using TrialEngine.Assets;

namespace TrialEngine.Debate;

public partial class CharacterActor : TextureRect
{
    private readonly Dictionary<string, Texture2D> _poses = new();

    public string CharacterId { get; private set; } = string.Empty;
    public string CurrentPose { get; private set; } = string.Empty;

    public void LoadCharacter(string characterId, IAssetLoader loader)
    {
        CharacterId = characterId;
        _poses.Clear();

        foreach (var pair in loader.LoadCharacterSprites(characterId))
            _poses[pair.Key] = pair.Value;

        if (_poses.Count > 0)
            SetPose(_poses.Keys.First());
    }

    public void SetPose(string pose)
    {
        if (!_poses.TryGetValue(pose, out var texture))
            return;

        CurrentPose = pose;
        Texture = texture;
    }
}
