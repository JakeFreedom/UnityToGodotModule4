using Godot;
using System;
using System.Numerics;
using System.Reflection;

public partial class CoinDrop1 : Node3D
{
	[Export] private float rotationSpeed = 10;
	[Export(PropertyHint.Range, ".5, 3,.1")] private float fallingSpeed = 3;
	[Export(PropertyHint.Range, "1,5,1")] private int pointValue = 1;

    private Line2D _line;
	public override void _Ready()
	{
		GetNode<Area3D>("Area3D").Name = "Drop 1";
		this.Name = "Drop 1";

        _line = GetNode<Line2D>("Line2D");
        _line.Width = 5.0f;
        _line.DefaultColor = Colors.Green;
        _line.BeginCapMode = Line2D.LineCapMode.Round;
        _line.EndCapMode = Line2D.LineCapMode.Round;
        _line.JointMode = Line2D.LineJointMode.Round;

        _line.AddPoint(new Godot.Vector2(50, 50));
        _line.AddPoint(new Godot.Vector2(200, 100));
        _line.AddPoint(new Godot.Vector2(350, 50));
        _line.AddPoint(new Godot.Vector2(500, 150));
    }
}
