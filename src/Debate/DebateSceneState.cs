using Godot;
using TrialEngine.Core;
using TrialEngine.Utils;

namespace TrialEngine.Debate;

public sealed class DebateSceneState : IGameState
{
    public TrialPhase Phase => TrialPhase.NonstopDebate;

    private readonly ContradictionEngine _engine = new();
    private DebateSpawner? _spawner;

    public void Enter(GameContext context)
    {
        context.Events.PublishDebug("Entering Nonstop Debate.");

        var debateLayer = context.SceneDirector.GetDebateLayer();
        debateLayer.Visible = true;
        context.SceneDirector.GetVNLayer().Visible = false;

        var container = debateLayer.GetNodeOrNull<Control>("DebateTextContainer") ?? debateLayer;
        _spawner = new DebateSpawner(container);

        var script = JsonLoader.LoadFromGodotPath<DebateScript>("res://data/debate_math_trial_001.json") ?? new DebateScript();
        _engine.Load(script);
        _spawner.Spawn(_engine.Statements);

        _engine.WeakPointBroken += wp =>
        {
            context.Audio.PlaySfx("weak_break");
            context.Events.PublishDebug($"Weak point broken: {wp.Text}");
        };

        _engine.WrongHit += wp =>
        {
            context.Audio.PlaySfx("wrong_hit");
            context.Events.PublishDebug($"Wrong bullet: {wp.Text}");
        };

        _engine.NoiseSilenced += wp =>
        {
            context.Audio.PlaySfx("weak_break");
            context.Events.PublishDebug($"Noise silenced: {wp.Text}");
        };

        _engine.DebateCleared += () =>
        {
            context.Audio.PlaySfx("weak_break");
            context.Events.PublishDebateCleared();
        };
    }

    public void Exit(GameContext context)
    {
        _spawner?.Clear();
        var debateLayer = context.SceneDirector.GetDebateLayer();
        debateLayer.Visible = false;
    }

    public void Update(GameContext context, double delta)
    {
        var d = (float)delta;
        context.Focus.SetActive(Input.IsActionPressed("focus"));
        _spawner?.Tick(d, context.Focus);

        if (Input.IsActionJustPressed("fire_truth"))
            TryFireTruth(context);

        if (Input.IsActionJustPressed("fire_silencer"))
            TryFireSilencer(context);
    }

    public void PhysicsUpdate(GameContext context, double delta) { }

    private void TryFireTruth(GameContext context)
    {
        if (_spawner == null)
            return;

        var hit = _spawner.HitTest(context.Root.GetViewport().GetMousePosition());
        if (hit?.BoundWeakPoint == null)
            return;

        context.Audio.PlaySfx("truth_fire");
        _engine.FireAt(hit.BoundWeakPoint, context.Clock);
    }

    private void TryFireSilencer(GameContext context)
    {
        if (_spawner == null)
            return;

        var hit = _spawner.HitTest(context.Root.GetViewport().GetMousePosition());
        if (hit?.BoundWeakPoint == null)
            return;

        context.Audio.PlaySfx("truth_fire");
        _engine.FireSilencerAt(hit.BoundWeakPoint, context.Clock);
    }
}
