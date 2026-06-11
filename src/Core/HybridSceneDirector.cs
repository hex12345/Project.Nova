using Godot;
using TrialEngine.Utils;

namespace TrialEngine.Core;

public partial class HybridSceneDirector : Node
{
    [Export] public NodePath World3DRootPath { get; set; } = new("World3DRoot");
    [Export] public NodePath UI2DRootPath { get; set; } = new("UI2DRoot");
    [Export] public NodePath FadeRectPath { get; set; } = new("TransitionLayer/FadeRect");
    [Export] public NodePath LogicDiveCameraPath { get; set; } = new("World3DRoot/LogicDiveCamera");

    public Node3D World3DRoot { get; private set; } = null!;
    public Control UI2DRoot { get; private set; } = null!;
    public ColorRect FadeRect { get; private set; } = null!;
    public Camera3D? LogicDiveCamera { get; private set; }

    public bool IsTransitioning { get; private set; }

    public override void _Ready()
    {
        World3DRoot = NodePathGuard.GetRequired<Node3D>(this, World3DRootPath);
        UI2DRoot = NodePathGuard.GetRequired<Control>(this, UI2DRootPath);
        FadeRect = NodePathGuard.GetRequired<ColorRect>(this, FadeRectPath);
        LogicDiveCamera = GetNodeOrNull<Camera3D>(LogicDiveCameraPath);

        World3DRoot.Visible = false;
        UI2DRoot.Visible = true;
        SetFadeAlpha(0f);
    }

    public async Task TransitionTo3D()
    {
        if (IsTransitioning) return;
        IsTransitioning = true;

        await FadeTo(1f, 0.20f);
        UI2DRoot.Visible = false;
        World3DRoot.Visible = true;
        if (LogicDiveCamera != null)
            LogicDiveCamera.Current = true;
        await FadeTo(0f, 0.20f);

        IsTransitioning = false;
    }

    public async Task TransitionTo2D()
    {
        if (IsTransitioning) return;
        IsTransitioning = true;

        await FadeTo(1f, 0.20f);
        World3DRoot.Visible = false;
        UI2DRoot.Visible = true;
        await FadeTo(0f, 0.20f);

        IsTransitioning = false;
    }

    public Control GetDebateLayer() => UI2DRoot.GetNode<Control>("DebateLayer");
    public Control GetVNLayer() => UI2DRoot.GetNode<Control>("VNLayer");
    public Control GetHudLayer() => UI2DRoot.GetNode<Control>("HudLayer");

    private async Task FadeTo(float alpha, float duration)
    {
        var tween = CreateTween();
        tween.TweenProperty(FadeRect, "modulate:a", alpha, duration);
        await ToSignal(tween, Tween.SignalName.Finished);
    }

    private void SetFadeAlpha(float alpha)
    {
        var color = FadeRect.Modulate;
        color.A = alpha;
        FadeRect.Modulate = color;
    }
}
