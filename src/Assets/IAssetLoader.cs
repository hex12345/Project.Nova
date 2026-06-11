using Godot;

namespace TrialEngine.Assets;

public interface IAssetLoader
{
    Texture2D? LoadTexture(string path);
    AudioStream? LoadAudio(string path);
    CharacterAssetEntry? GetCharacter(string characterId);
    IReadOnlyDictionary<string, Texture2D> LoadCharacterSprites(string characterId);
    AudioStream? GetSfx(string id);
}
