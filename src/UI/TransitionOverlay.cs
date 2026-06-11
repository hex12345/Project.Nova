using Godot;

namespace TrialEngine.UI;

public partial class TransitionOverlay : CanvasLayer
{
    [Export] public NodePath FadeRectPath { get; set; } = new("FadeRect");
    public ColorRect? FadeRect { get; private set; }

    public override void _Ready()
    {
        FadeRect = GetNodeOrNull<ColorRect>(FadeRectPath);
    }
}
