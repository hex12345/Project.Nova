namespace TrialEngine.Core;

public sealed class EventBus
{
    public event Action<TrialPhase>? PhaseChanged;
    public event Action<string>? DebugMessage;
    public event Action? VisualNovelCleared;
    public event Action? DebateCleared;
    public event Action? LogicDiveCleared;
    public event Action? TrialFailed;

    public void PublishPhaseChanged(TrialPhase phase) => PhaseChanged?.Invoke(phase);
    public void PublishDebug(string message) => DebugMessage?.Invoke(message);
    public void PublishVisualNovelCleared() => VisualNovelCleared?.Invoke();
    public void PublishDebateCleared() => DebateCleared?.Invoke();
    public void PublishLogicDiveCleared() => LogicDiveCleared?.Invoke();
    public void PublishTrialFailed() => TrialFailed?.Invoke();
}
