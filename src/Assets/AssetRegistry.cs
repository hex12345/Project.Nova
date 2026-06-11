using Godot;

namespace TrialEngine.Assets;

public sealed class AssetRegistry
{
    public CharacterManifest Characters { get; }
    public AudioManifest Audio { get; }

    private readonly Dictionary<string, Texture2D> _textureCache = new();
    private readonly Dictionary<string, AudioStream> _audioCache = new();

    public AssetRegistry(CharacterManifest characters, AudioManifest audio)
    {
        Characters = characters;
        Audio = audio;
    }

    public bool TryGetTexture(string path, out Texture2D texture) => _textureCache.TryGetValue(path, out texture!);
    public void CacheTexture(string path, Texture2D texture) => _textureCache[path] = texture;

    public bool TryGetAudio(string path, out AudioStream audio) => _audioCache.TryGetValue(path, out audio!);
    public void CacheAudio(string path, AudioStream stream) => _audioCache[path] = stream;
}
