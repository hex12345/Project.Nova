using Godot;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrialEngine.Utils;

public static class JsonLoader
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static T? LoadFromGodotPath<T>(string path)
    {
        try
        {
            string json;
            if (path.StartsWith("res://") || path.StartsWith("user://"))
                json = Godot.FileAccess.GetFileAsString(path);
            else
                json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, Options);
        }
        catch (Exception ex)
        {
            GD.PushWarning($"Could not load JSON {path}: {ex.Message}");
            return default;
        }
    }
}
