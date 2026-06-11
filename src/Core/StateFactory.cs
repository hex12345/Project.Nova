using TrialEngine.Debate;
using TrialEngine.LogicDive;
using TrialEngine.VN;

namespace TrialEngine.Core;

public static class StateFactory
{
    public static IGameState Create(TrialPhase phase)
    {
        return phase switch
        {
            TrialPhase.VisualNovel => new VisualNovelSceneState(),
            TrialPhase.NonstopDebate => new DebateSceneState(),
            TrialPhase.LogicDive => new LogicDiveSceneState(),
            TrialPhase.Result => new ResultState(),
            TrialPhase.GameOver => new GameOverState(),
            _ => new VisualNovelSceneState()
        };
    }
}

public sealed class ResultState : IGameState
{
    public TrialPhase Phase => TrialPhase.Result;
    public void Enter(GameContext context) => context.Events.PublishDebug("Trial result state entered.");
    public void Exit(GameContext context) { }
    public void Update(GameContext context, double delta) { }
    public void PhysicsUpdate(GameContext context, double delta) { }
}

public sealed class GameOverState : IGameState
{
    public TrialPhase Phase => TrialPhase.GameOver;
    public void Enter(GameContext context)
    {
        context.Events.PublishDebug("Trial failed. Timer expired or influence collapsed.");
        context.Events.PublishTrialFailed();
    }
    public void Exit(GameContext context) { }
    public void Update(GameContext context, double delta) { }
    public void PhysicsUpdate(GameContext context, double delta) { }
}
