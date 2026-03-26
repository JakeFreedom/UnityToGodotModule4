using Godot;
using System;

public partial class Despawn : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += AreaEnteredHandler;

	}


	private void AreaEnteredHandler(Area3D otherArea) {

		((GenericDrop)otherArea.Owner).EmitSignal("IHaveBeenCaught", otherArea.Owner);
		otherArea.Owner.QueueFree();
	}

}
