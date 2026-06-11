using Godot;
using TrialEngine.Utils;

namespace TrialEngine.Assets;

public sealed class LocalAssetLoader : IAssetLoader
{
    private readonly AssetRegistry _registry;

    public LocalAssetLoader(string characterManifestPath, string audioManifestPath)
    {
        var characters = JsonLoader.LoadFromGodotPath<CharacterManifest>(characterManifestPath) ?? new CharacterManifest();
        var audio = JsonLoader.LoadFromGodotPath<AudioManifest>(audioManifestPath) ?? new AudioManifest();
        _registry = new AssetRegistry(characters, audio);
    }

    public CharacterAssetEntry? GetCharacter(string characterId)
    {
        return _registry.Characters.Characters.TryGetValue(characterId, out var entry) ? entry : null;
    }

    public IReadOnlyDictionary<string, Texture2D> LoadCharacterSprites(string characterId)
    {
        var result = new Dictionary<string, Texture2D>();
        var character = GetCharacter(characterId);

        if (character == null)
            return result;

        foreach (var pair in character.Sprites)
        {
            var texture = LoadTexture(pair.Value);
            if (texture != null)
                result[pair.Key] = texture;
        }

        return result;
    }

    public Texture2D? LoadTexture(string path)
    {
        if (_registry.TryGetTexture(path, out var cached))
            return cached;

        Texture2D? texture = null;

        if (path.StartsWith("res://") || path.StartsWith("user://"))
        {
            texture = ResourceLoader.Load<Texture2D>(path);
        }

        if (texture == null)
        {
            var fullPath = ProjectSettings.GlobalizePath(path);
            if (!File.Exists(fullPath))
                return null;

            var image = Image.LoadFromFile(fullPath);
            if (image == null || image.IsEmpty())
                return null;

            texture = ImageTexture.CreateFromImage(image);
        }

        _registry.CacheTexture(path, texture);
        return texture;
    }

    public AudioStream? LoadAudio(string path)
    {
        if (_registry.TryGetAudio(path, out var cached))
            return cached;

        AudioStream? audio = null;

        if (path.StartsWith("res://") || path.StartsWith("user://"))
            audio = ResourceLoader.Load<AudioStream>(path);

        if (audio == null)
        {
            var fullPath = ProjectSettings.GlobalizePath(path);
            if (!File.Exists(fullPath))
                return null;

            audio = ResourceLoader.Load<AudioStream>(fullPath);
        }

        if (audio != null)
            _registry.CacheAudio(path, audio);

        return audio;
    }

    public AudioStream? GetSfx(string id)
    {
        if (!_registry.Audio.Sfx.TryGetValue(id, out var path))
            return null;

        return LoadAudio(path);
    }
}
