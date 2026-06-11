namespace TrialEngine.Core;

public sealed class TrialClock
{
    public float RemainingSeconds { get; private set; }
    public bool IsExpired => RemainingSeconds <= 0f;
    public bool IsRunning { get; private set; }

    public event Action? Expired;
    public event Action<float>? Changed;

    public TrialClock(float initialSeconds)
    {
        RemainingSeconds = MathF.Max(0f, initialSeconds);
    }

    public void Start() => IsRunning = true;
    public void Stop() => IsRunning = false;

    public void Reset(float seconds)
    {
        RemainingSeconds = MathF.Max(0f, seconds);
        Changed?.Invoke(RemainingSeconds);
    }

    public void AddSeconds(float seconds)
    {
        RemainingSeconds = MathF.Max(0f, RemainingSeconds + seconds);
        Changed?.Invoke(RemainingSeconds);

        if (RemainingSeconds <= 0f)
            ForceExpire();
    }

    public void Tick(float delta)
    {
        if (!IsRunning || IsExpired)
            return;

        RemainingSeconds = MathF.Max(0f, RemainingSeconds - delta);
        Changed?.Invoke(RemainingSeconds);

        if (RemainingSeconds <= 0f)
            ForceExpire();
    }

    private void ForceExpire()
    {
        if (!IsRunning && IsExpired)
            return;

        RemainingSeconds = 0f;
        IsRunning = false;
        Expired?.Invoke();
    }
}
