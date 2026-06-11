using Godot;
using TrialEngine.Core;

namespace TrialEngine.Debate;

public partial class DebateTextActor : Label
{
    public WeakPoint? BoundWeakPoint { get; private set; }
    public bool IsDead => BoundWeakPoint?.IsBroken == true;

    private Vector2 _velocity;
    private float _jitterStrength;
    private float _rotationStrength;
    private float _age;

    public void Bind(WeakPoint weakPoint, Vector2 startPosition, Vector2 velocity, float jitterStrength, float rotationStrength)
    {
        BoundWeakPoint = weakPoint;
        Text = weakPoint.Text;
        Position = startPosition;
        _velocity = velocity;
        _jitterStrength = jitterStrength;
        _rotationStrength = rotationStrength;
        PivotOffset = Size / 2f;
    }

    public void Tick(float delta, FocusGauge focus)
    {
        if (BoundWeakPoint == null)
            return;

        _age += delta;
        float focusScale = focus.SlowMultiplier;

        Vector2 jitter = new(
            MathF.Sin(_age * 18f) * _jitterStrength,
            MathF.Cos(_age * 21f) * _jitterStrength
        );

        Position += (_velocity * delta * focusScale) + jitter * delta;
        RotationDegrees = MathF.Sin(_age * 8f) * _rotationStrength * focusScale;
        Modulate = BoundWeakPoint.IsBroken ? new Color(0.5f, 0.5f, 0.5f, 0.35f) : Colors.White;
    }

    public bool HitTest(Vector2 point)
    {
        var rect = new Rect2(GlobalPosition, Size);
        return rect.HasPoint(point);
    }
}
