namespace TrialEngine.LogicDive;

public sealed class RewindBuffer
{
    private readonly LinkedList<PhysicsSnapshot> _snapshots = new();
    private float _captureAccumulator;

    public float CaptureIntervalSeconds { get; }
    public float MaxHistorySeconds { get; }
    public int Count => _snapshots.Count;

    public RewindBuffer(float captureIntervalSeconds = 1f, float maxHistorySeconds = 12f)
    {
        CaptureIntervalSeconds = MathF.Max(0.05f, captureIntervalSeconds);
        MaxHistorySeconds = MathF.Max(CaptureIntervalSeconds, maxHistorySeconds);
    }

    public void Tick(float delta, Func<PhysicsSnapshot> capture)
    {
        _captureAccumulator += delta;

        while (_captureAccumulator >= CaptureIntervalSeconds)
        {
            _captureAccumulator -= CaptureIntervalSeconds;
            Push(capture());
        }
    }

    public void Push(PhysicsSnapshot snapshot)
    {
        _snapshots.AddLast(snapshot);
        int maxCount = Math.Max(1, (int)MathF.Ceiling(MaxHistorySeconds / CaptureIntervalSeconds));

        while (_snapshots.Count > maxCount)
            _snapshots.RemoveFirst();
    }

    public bool TryPop(out PhysicsSnapshot snapshot)
    {
        if (_snapshots.Last == null)
        {
            snapshot = default;
            return false;
        }

        snapshot = _snapshots.Last.Value;
        _snapshots.RemoveLast();
        return true;
    }

    public void Clear()
    {
        _snapshots.Clear();
        _captureAccumulator = 0f;
    }
}
