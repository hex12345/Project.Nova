using Godot;
using TrialEngine.Core;

namespace TrialEngine.UI;

public partial class FocusHud : ProgressBar
{
    public void Bind(FocusGauge focus)
    {
        MinValue = 0;
        MaxValue = 100;
        Value = focus.Normalized * 100;
        focus.Changed += v => Value = v * 100;
    }
}
