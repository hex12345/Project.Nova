using Godot;

namespace TrialEngine.LogicDive;

public partial class TubePathBuilder : Node3D
{
    [Export] public int SegmentCount { get; set; } = 48;
    [Export] public float SegmentLength { get; set; } = 8f;
    [Export] public float Radius { get; set; } = 7f;
    [Export] public int RingResolution { get; set; } = 24;

    public override void _Ready()
    {
        BuildPlaceholderTube();
    }

    public void BuildPlaceholderTube()
    {
        var meshInstance = GetNodeOrNull<MeshInstance3D>("GeneratedTube") ?? new MeshInstance3D { Name = "GeneratedTube" };
        if (meshInstance.GetParent() == null)
            AddChild(meshInstance);

        var arrayMesh = new ArrayMesh();
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        for (int z = 0; z <= SegmentCount; z++)
        {
            float depth = -z * SegmentLength;
            for (int r = 0; r < RingResolution; r++)
            {
                float t = r / (float)RingResolution * MathF.Tau;
                vertices.Add(new Vector3(MathF.Cos(t) * Radius, MathF.Sin(t) * Radius, depth));
            }
        }

        for (int z = 0; z < SegmentCount; z++)
        {
            for (int r = 0; r < RingResolution; r++)
            {
                int a = z * RingResolution + r;
                int b = z * RingResolution + (r + 1) % RingResolution;
                int c = (z + 1) * RingResolution + r;
                int d = (z + 1) * RingResolution + (r + 1) % RingResolution;

                indices.Add(a); indices.Add(c); indices.Add(b);
                indices.Add(b); indices.Add(c); indices.Add(d);
            }
        }

        var arrays = new Godot.Collections.Array();
        arrays.Resize((int)Mesh.ArrayType.Max);
        arrays[(int)Mesh.ArrayType.Vertex] = vertices.ToArray();
        arrays[(int)Mesh.ArrayType.Index] = indices.ToArray();
        arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
        meshInstance.Mesh = arrayMesh;
    }
}
