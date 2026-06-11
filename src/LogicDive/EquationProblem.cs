using System.Text.Json.Serialization;

namespace TrialEngine.LogicDive;

public sealed class LogicDiveProblemSet
{
    [JsonPropertyName("setId")]
    public string SetId { get; set; } = string.Empty;

    [JsonPropertyName("problems")]
    public List<EquationProblem> Problems { get; set; } = new();
}

public sealed class EquationProblem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;

    [JsonPropertyName("answers")]
    public List<BranchAnswer> Answers { get; set; } = new();
}

public sealed class BranchAnswer
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }

    [JsonPropertyName("lane")]
    public int Lane { get; set; }
}
