using Catchem2D;
using Catchem2D.Events;
using Godot;
using System;

public partial class PlayerSprite : CharacterBody2D
{
	[Export] float BaseSpeed = 900.0f;
	[Export] float BoostSpeed = 4000.0f;

	[Signal] public delegate void ItemCaughtEventHandler(int score);

	Boolean isEnabled = true;
	public override void _Ready()
	{
		GetNode<Area2D>("Area2D").AreaEntered += ObjectEntered;
		//GameConfig.Instance.GetBus().Subscribe<BallCaughtEvent>(OnBallCaught);
		GameConfig.Instance.GetBus().Subscribe<GameOverEvent>(OnGameOver);
	}


	public void OnBallCaught(BallCaughtEvent evt) {

		GD.Print("Ball caught");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		float Speed = BaseSpeed;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			Speed = CheckForSpeedBoost(Speed);
			velocity.X = direction.X * Speed;
		}
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		Velocity = velocity;
		if(isEnabled)
			MoveAndSlide();
	}

	private void ObjectEntered(Area2D otherObject)
	{
		//GD.Print(otherObject.Owner.Name);
		EmitSignal(SignalName.ItemCaught, ((DroppedObject)otherObject.Owner).CurrentHealth+1);
		//otherObject.Owner.GetNode<RigidBody2D>("RigidBody2D").Freeze = true;
		otherObject.Owner.GetNode<AnimationPlayer>("AnimationPlayer").Play("new_animation");
		((DroppedObject)otherObject.Owner).Alive = false;
		GameConfig.Instance.GetBus().Publish<BallCaughtEvent>(new BallCaughtEvent(otherObject.Owner.Name));


	}

	private void OnGameOver(GameOverEvent evt)
	{
		isEnabled = false;
        GetNode<Area2D>("Area2D").AreaEntered -= ObjectEntered;
    }

	private float CheckForSpeedBoost(float Speed) {
		if (Input.IsKeyPressed(Key.Shift))
			Speed = BoostSpeed;
		else
			Speed = BaseSpeed;

		return Speed;
    }
}
