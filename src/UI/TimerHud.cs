using Godot;
using TrialEngine.Core;

namespace TrialEngine.UI;

public partial class TimerHud : Label
{
    public void Bind(TrialClock clock)
    {
        clock.Changed += UpdateText;
        UpdateText(clock.RemainingSeconds);
    }

    private void UpdateText(float seconds)
    {
        int min = (int)(seconds / 60f);
        int sec = (int)(seconds % 60f);
        Text = $"{min:00}:{sec:00}";
    }
}
