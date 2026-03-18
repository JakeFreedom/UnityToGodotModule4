using Godot;
using System;

public partial class Main : Node2D
{
	[Export] Label score;

	int playerScore;
	PlayerSprite player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<PlayerSprite>("Player/PlayerSprite");
		player.ItemCaught += ChangeScore;
		playerScore = 0;
		score.Text = playerScore.ToString();
	}

	private void ChangeScore(int scoreToAdd) {

		playerScore += scoreToAdd;
		score.Text = playerScore.ToString();

	}
}
