namespace TrialEngine.Utils;

public static class MathTextFormatter
{
    public static string NormalizeForDisplay(string text)
    {
        return text
            .Replace("sqrt", "√")
            .Replace("^2", "²")
            .Replace(">=", "≥")
            .Replace("<=", "≤")
            .Replace("!=", "≠");
    }
}
