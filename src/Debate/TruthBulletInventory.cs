namespace TrialEngine.Debate;

public sealed class TruthBulletInventory
{
    private readonly List<TruthBullet> _bullets = new();
    private int _selectedIndex;

    public IReadOnlyList<TruthBullet> Bullets => _bullets;
    public TruthBullet? Selected => _bullets.Count == 0 ? null : _bullets[_selectedIndex];

    public event Action<TruthBullet?>? SelectionChanged;

    public void Load(IEnumerable<TruthBullet> bullets)
    {
        _bullets.Clear();
        _bullets.AddRange(bullets);
        _selectedIndex = 0;
        SelectionChanged?.Invoke(Selected);
    }

    public void SelectById(string id)
    {
        int index = _bullets.FindIndex(b => string.Equals(b.Id, id, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
            return;

        _selectedIndex = index;
        SelectionChanged?.Invoke(Selected);
    }

    public void SelectNext()
    {
        if (_bullets.Count == 0)
            return;

        _selectedIndex = (_selectedIndex + 1) % _bullets.Count;
        SelectionChanged?.Invoke(Selected);
    }

    public void SelectPrevious()
    {
        if (_bullets.Count == 0)
            return;

        _selectedIndex = (_selectedIndex - 1 + _bullets.Count) % _bullets.Count;
        SelectionChanged?.Invoke(Selected);
    }
}
