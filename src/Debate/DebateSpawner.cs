using Godot;

namespace TrialEngine.Debate;

public sealed class DebateSpawner
{
    private readonly Control _container;
    private readonly List<DebateTextActor> _actors = new();

    public IReadOnlyList<DebateTextActor> Actors => _actors;

    public DebateSpawner(Control container)
    {
        _container = container;
    }

    public void Spawn(IEnumerable<DebateStatement> statements)
    {
        Clear();
        int index = 0;

        foreach (var statement in statements)
        {
            foreach (var weakPoint in statement.WeakPoints)
            {
                var actor = new DebateTextActor
                {
                    Name = $"WeakPoint_{weakPoint.Id}",
                    Text = weakPoint.Text,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Size = new Vector2(360, 48)
                };

                _container.AddChild(actor);

                float y = 110 + index * 72;
                var start = new Vector2(1100 + index * 60, y);
                var velocity = new Vector2(-statement.Speed, 0f);

                actor.Bind(weakPoint, start, velocity, statement.JitterStrength, statement.RotationStrength);
                _actors.Add(actor);
                index++;
            }
        }
    }

    public DebateTextActor? HitTest(Vector2 screenPoint)
    {
        for (int i = _actors.Count - 1; i >= 0; i--)
        {
            var actor = _actors[i];
            if (actor.BoundWeakPoint == null || actor.BoundWeakPoint.IsBroken)
                continue;

            if (actor.HitTest(screenPoint))
                return actor;
        }

        return null;
    }

    public void Tick(float delta, Core.FocusGauge focus)
    {
        foreach (var actor in _actors)
            actor.Tick(delta, focus);
    }

    public void Clear()
    {
        foreach (var actor in _actors)
            actor.QueueFree();

        _actors.Clear();
    }
}
