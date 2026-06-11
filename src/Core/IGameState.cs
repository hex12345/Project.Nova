namespace TrialEngine.Core;

public interface IGameState
{
    TrialPhase Phase { get; }
    void Enter(GameContext context);
    void Exit(GameContext context);
    void Update(GameContext context, double delta);
    void PhysicsUpdate(GameContext context, double delta);
}
