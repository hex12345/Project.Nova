using Godot;
using TrialEngine.Core;

namespace TrialEngine.LogicDive;

public partial class LogicDiveRunner : CharacterBody3D
{
    [Export] public float BaseSpeed { get; set; } = 26f;
    [Export] public float LaneDistance { get; set; } = 4f;
    [Export] public float LaneSwitchSpeed { get; set; } = 14f;
    [Export] public float Gravity { get; set; } = 12f;

    public float CurrentSpeed { get; private set; }
    public int CurrentLane { get; private set; }
    public RewindBuffer Rewind { get; private set; } = new();

    private Vector3 _verticalVelocity;
    private FocusGauge? _focus;

    public override void _Ready()
    {
        CurrentSpeed = BaseSpeed;
    }

    public void Configure(Core.LogicDiveConfig config, FocusGauge focus)
    {
        BaseSpeed = config.BaseSpeed;
        LaneDistance = config.LaneDistance;
        CurrentSpeed = BaseSpeed;
        Rewind = new RewindBuffer(config.RewindCaptureIntervalSeconds, config.RewindHistorySeconds);
        _focus = focus;
    }

    public override void _PhysicsProcess(double delta)
    {
        float d = (float)delta;
        Rewind.Tick(d, CaptureSnapshot);
        HandleInput();
        MoveRunner(d);
    }

    public void ResetRunner(Vector3 position)
    {
        GlobalPosition = position;
        CurrentSpeed = BaseSpeed;
        CurrentLane = 0;
        Velocity = Vector3.Zero;
        Rewind.Clear();
    }

    public void ApplyLogicErrorPenalty()
    {
        CurrentSpeed = MathF.Max(BaseSpeed * 0.65f, CurrentSpeed * 0.85f);
        RewindOneStep();
    }

    public void RewindOneStep()
    {
        if (!Rewind.TryPop(out var snapshot))
            return;

        GlobalPosition = snapshot.Position;
        Velocity = snapshot.Velocity;
        GlobalTransform = new Transform3D(new Basis(snapshot.Rotation), snapshot.Position);
        CurrentSpeed = snapshot.Speed;
        CurrentLane = snapshot.Lane;
    }

    private void HandleInput()
    {
        if (Input.IsActionJustPressed("lane_left"))
            CurrentLane = Math.Max(-1, CurrentLane - 1);

        if (Input.IsActionJustPressed("lane_right"))
            CurrentLane = Math.Min(1, CurrentLane + 1);

        if (Input.IsActionJustPressed("rewind"))
            RewindOneStep();
    }

    private void MoveRunner(float delta)
    {
        float targetX = CurrentLane * LaneDistance;
        var pos = GlobalPosition;
        pos.X = Mathf.Lerp(pos.X, targetX, LaneSwitchSpeed * delta);
        GlobalPosition = pos;

        float speedScale = _focus?.SlowMultiplier ?? 1f;
        var forward = -GlobalTransform.Basis.Z;

        if (!IsOnFloor())
            _verticalVelocity += Vector3.Down * Gravity * delta;
        else
            _verticalVelocity = Vector3.Zero;

        Velocity = forward * CurrentSpeed * speedScale + _verticalVelocity;
        MoveAndSlide();
    }

    private PhysicsSnapshot CaptureSnapshot()
    {
        return new PhysicsSnapshot(
            GlobalPosition,
            Velocity,
            GlobalTransform.Basis.GetRotationQuaternion(),
            CurrentSpeed,
            CurrentLane,
            Time.GetTicksMsec() / 1000.0
        );
    }
}
