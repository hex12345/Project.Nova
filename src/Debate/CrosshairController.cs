using Godot;

namespace TrialEngine.Debate;

public partial class CrosshairController : Control
{
    [Export] public float FollowStrength { get; set; } = 30f;

    public override void _Process(double delta)
    {
        var mouse = GetViewport().GetMousePosition();
        Position = Position.Lerp(mouse - Size / 2f, FollowStrength * (float)delta);
    }
}
