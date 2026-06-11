using Godot;

namespace TrialEngine.LogicDive;

public partial class LogicDiveHud : Control
{
    [Export] public NodePath PromptLabelPath { get; set; } = new("PromptLabel");
    private Label? _promptLabel;

    public override void _Ready()
    {
        _promptLabel = GetNodeOrNull<Label>(PromptLabelPath);
    }

    public void SetPrompt(string text)
    {
        if (_promptLabel != null)
            _promptLabel.Text = text;
    }
}
