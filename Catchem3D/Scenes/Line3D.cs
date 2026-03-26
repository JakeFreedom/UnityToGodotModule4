using Godot;
using System;

public partial class Line3D : MeshInstance3D
{
    private ImmediateMesh _mesh;

    public override void _Ready()
    {
        _mesh = new ImmediateMesh();
        Mesh = _mesh;

        // Optional: make it unshaded so the color shows clearly
        var mat = new StandardMaterial3D();
        mat.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
        mat.AlbedoColor = Colors.Green;
       
        MaterialOverride = mat;

        DrawLine(new Vector3(GetParent<GenericDrop>().Position.X, GetParent<GenericDrop>().Position.Y-3, 0), 
            new Vector3(GetParent<GenericDrop>().Position.X, -50, 0));
    }

    public void DrawLine(Vector3 from, Vector3 to)
    {
        _mesh.ClearSurfaces();
        _mesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
        _mesh.SurfaceAddVertex(from);
        _mesh.SurfaceAddVertex(to);
        _mesh.SurfaceEnd();
    }
}
