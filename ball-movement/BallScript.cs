using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;

public partial class BallScript : Node3D
{

	[Export] Node3D pointa;
	[Export] Node3D pointb;
	float time;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartPingPong();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		await MoveObject(delta);
	}

	private async Task MoveObject(double delta) 
	{
		time += (float)delta / 2;
		//time = Mathf.Clamp(time, 0, 1);
		//GlobalPosition = pointa.GlobalPosition.Lerp(pointb.GlobalPosition,time);
		//if(time >= 1) { }

		//float t = Mathf.PingPong(time, 1.0f);
		//GlobalPosition = pointa.GlobalPosition.Lerp(pointb.GlobalPosition, t);	
		//GD.Print(time);
		//await Task.Delay(5000);
		//GD.Print($"New Time {Time.GetTicksMsec().ToString()}");
	}


    private void StartPingPong()
    {
        var tween = CreateTween().SetLoops();
		tween.TweenProperty(this, "global_position", pointb.GlobalPosition, 5.5f)
			.From(pointa.GlobalPosition)
			.SetTrans(Tween.TransitionType.Sine);
		//.SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(this, "global_position", pointa.GlobalPosition, 1.0f)
			.SetTrans(Tween.TransitionType.Sine);
            //.SetEase(Tween.EaseType.InOut);
    }
}
