using Godot;

namespace TrialEngine.LogicDive;

public readonly struct PhysicsSnapshot
{
    public Vector3 Position { get; }
    public Vector3 Velocity { get; }
    public Quaternion Rotation { get; }
    public float Speed { get; }
    public int Lane { get; }
    public double TimeStamp { get; }

    public PhysicsSnapshot(Vector3 position, Vector3 velocity, Quaternion rotation, float speed, int lane, double timeStamp)
    {
        Position = position;
        Velocity = velocity;
        Rotation = rotation;
        Speed = speed;
        Lane = lane;
        TimeStamp = timeStamp;
    }
}
