using TrialEngine.Core;

namespace TrialEngine.Debate;

public sealed class ContradictionEngine
{
    private readonly List<DebateStatement> _statements = new();
    public IReadOnlyList<DebateStatement> Statements => _statements;
    public TruthBulletInventory Inventory { get; } = new();

    public event Action<WeakPoint>? WeakPointBroken;
    public event Action<WeakPoint>? WrongHit;
    public event Action<WeakPoint>? NoiseSilenced;
    public event Action? DebateCleared;

    public void Load(DebateScript script)
    {
        _statements.Clear();
        _statements.AddRange(script.Statements);
        Inventory.Load(script.TruthBullets);
    }

    public void FireAt(WeakPoint weakPoint, TrialClock clock)
    {
        var bullet = Inventory.Selected;
        if (bullet == null)
            return;

        if (weakPoint.Type == WeakPointType.WhiteNoise)
        {
            WrongHit?.Invoke(weakPoint);
            clock.AddSeconds(-1.5f);
            return;
        }

        if (weakPoint.TryBreak(bullet))
        {
            WeakPointBroken?.Invoke(weakPoint);
            if (AllRequiredWeakPointsBroken())
                DebateCleared?.Invoke();
            return;
        }

        WrongHit?.Invoke(weakPoint);
        clock.AddSeconds(-5f);
    }

    public void FireSilencerAt(WeakPoint weakPoint, TrialClock clock)
    {
        if (weakPoint.Type == WeakPointType.WhiteNoise)
        {
            weakPoint.ForceBreak();
            NoiseSilenced?.Invoke(weakPoint);
            clock.AddSeconds(1f);
            return;
        }

        clock.AddSeconds(-2f);
        WrongHit?.Invoke(weakPoint);
    }

    private bool AllRequiredWeakPointsBroken()
    {
        return _statements
            .SelectMany(s => s.WeakPoints)
            .Where(w => w.Type is WeakPointType.Contradiction or WeakPointType.Agreement)
            .All(w => w.IsBroken);
    }
}
