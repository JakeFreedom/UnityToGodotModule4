using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

public partial class GenericDrop : Node3D
{
	Mesh genericMesh;
	float fallingSpeed = 10;
	float rotationalSpeed = 5;
	Boolean isCaught = false;
	Line2D _line;

	[Signal] public delegate void IHaveBeenCaughtEventHandler(GenericDrop me);

	public override void _Ready()
	{
		GetNode<MeshInstance3D>("Texture").Mesh = genericMesh;


    }

	double animationTime = 0;
    public override void _Process(double delta)
    {
        if(isCaught)
		{
			animationTime += delta;
			this.fallingSpeed = 0f;
			this.Scale -= new Godot.Vector3((float)delta, (float)delta, (float)delta);
			if (animationTime > .5) { //QueueFree(); 
				animationTime = 0;
				QueueFree();
			}
		}

    }

	public void YouHaveBeenCaptures()
	{
		this.fallingSpeed = 0;
		isCaught = true;
		//We can either move the object up and scale it down manually
		//Or do we make an animation?
		EmitSignal(SignalName.IHaveBeenCaught, this);
	}


	public float FallingSpeed { set => fallingSpeed = value; get => fallingSpeed; }
	public float RotationalSpeed { set => rotationalSpeed = value; get => rotationalSpeed; }
	public Mesh GenericMesh { set => genericMesh = value; get => genericMesh; }

}
