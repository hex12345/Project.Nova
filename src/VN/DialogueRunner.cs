using Godot;

namespace TrialEngine.VN;

public sealed class DialogueRunner
{
    private readonly Queue<DialogLine> _lines = new();

    public DialogLine? Current { get; private set; }
    public bool IsComplete => Current == null && _lines.Count == 0;

    public event Action<DialogLine>? LineChanged;
    public event Action? Completed;

    public void Load(IEnumerable<DialogLine> lines)
    {
        _lines.Clear();
        foreach (var line in lines)
            _lines.Enqueue(line);

        Advance();
    }

    public void Advance()
    {
        if (_lines.Count == 0)
        {
            Current = null;
            Completed?.Invoke();
            return;
        }

        Current = _lines.Dequeue();
        LineChanged?.Invoke(Current);
    }
}
