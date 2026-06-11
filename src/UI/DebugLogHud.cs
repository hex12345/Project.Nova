using Godot;
using TrialEngine.Core;

namespace TrialEngine.UI;

public partial class DebugLogHud : RichTextLabel
{
    [Export] public int MaxLines { get; set; } = 12;
    private readonly Queue<string> _lines = new();

    public void Bind(EventBus events)
    {
        events.DebugMessage += Push;
        events.PhaseChanged += phase => Push($"[phase] {phase}");
    }

    public void Push(string message)
    {
        _lines.Enqueue(message);
        while (_lines.Count > MaxLines)
            _lines.Dequeue();

        Text = string.Join("\n", _lines);
    }
}
