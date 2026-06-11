namespace TrialEngine.Core;

public sealed class FocusGauge
{
    public float Current { get; private set; }
    public float Max { get; }
    public bool IsActive { get; private set; }
    public float SlowMultiplier => IsActive ? 0.35f : 1.0f;
    public float Normalized => Max <= 0f ? 0f : Current / Max;

    private readonly float _drainPerSecond;
    private readonly float _regenPerSecond;

    public event Action<float>? Changed;
    public event Action<bool>? ActiveChanged;

    public FocusGauge(float max = 100f, float drainPerSecond = 28f, float regenPerSecond = 12f)
    {
        Max = MathF.Max(1f, max);
        Current = Max;
        _drainPerSecond = drainPerSecond;
        _regenPerSecond = regenPerSecond;
    }

    public void SetActive(bool active)
    {
        bool next = active && Current > 0f;
        if (next == IsActive)
            return;

        IsActive = next;
        ActiveChanged?.Invoke(IsActive);
    }

    public void SpendFlat(float amount)
    {
        Current = MathF.Max(0f, Current - amount);
        if (Current <= 0f)
            SetActive(false);
        Changed?.Invoke(Normalized);
    }

    public void Tick(float delta)
    {
        float before = Current;

        if (IsActive)
        {
            Current -= _drainPerSecond * delta;
            if (Current <= 0f)
            {
                Current = 0f;
                SetActive(false);
            }
        }
        else
        {
            Current = MathF.Min(Max, Current + _regenPerSecond * delta);
        }

        if (MathF.Abs(Current - before) > 0.001f)
            Changed?.Invoke(Normalized);
    }
}
