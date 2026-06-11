using Godot;
using TrialEngine.Core;

namespace TrialEngine.VN;

public sealed class VisualNovelSceneState : IGameState
{
    public TrialPhase Phase => TrialPhase.VisualNovel;

    private readonly DialogueRunner _runner = new();

    public void Enter(GameContext context)
    {
        context.SceneDirector.GetVNLayer().Visible = true;
        context.SceneDirector.GetDebateLayer().Visible = false;

        _runner.LineChanged += line => context.Events.PublishDebug($"{line.SpeakerId}: {line.Text}");
        _runner.Completed += () => context.Events.PublishVisualNovelCleared();

        _runner.Load(new[]
        {
            new DialogLine { SpeakerId = "makoto", Pose = "thinking", Text = "The contradiction is hidden inside the math." },
            new DialogLine { SpeakerId = "opponent_01", Pose = "neutral", Text = "Then prove it before the timer runs out." }
        });
    }

    public void Exit(GameContext context)
    {
        context.SceneDirector.GetVNLayer().Visible = false;
    }

    public void Update(GameContext context, double delta)
    {
        if (Input.IsActionJustPressed("advance_dialog"))
            _runner.Advance();
    }

    public void PhysicsUpdate(GameContext context, double delta) { }
}
