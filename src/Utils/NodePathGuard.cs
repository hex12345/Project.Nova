using Godot;

namespace TrialEngine.Utils;

public static class NodePathGuard
{
    public static T GetRequired<T>(Node owner, NodePath path) where T : Node
    {
        var node = owner.GetNodeOrNull<T>(path);
        if (node == null)
            throw new InvalidOperationException($"Required node missing: {path} on {owner.Name}");
        return node;
    }
}
