using Godot;

public partial class Ux : Control
{

	private int playerScore;
	private Label playerScoreLabel;
	AnimationPlayer centerText;
	private bool startCountDown = false;
	
	public override void _Ready()
	{
		playerScoreLabel = GetNode<Label>("MarginContainer/HBoxContainer/Score");
		centerText = GetNode<AnimationPlayer>("MoveTextToCenter");
		GameConfig.Instance.GameOver += GameOverHandler;
	}

	double timeDelay;
    public override void _Process(double delta)
    {
		
		if (GameConfig.IsGameOver && playerScore > 0 && startCountDown)
		{
            timeDelay += delta;
			if (timeDelay > .3)
			{
				playerScore -= 1;
				playerScoreLabel.Text = playerScore.ToString();
				timeDelay = 0;
			}

			if(playerScore == 0)
			{
				//Fade Text
				centerText.Play("FadePlayerScoreText");

			}
        }
    }

	public void UpdatePlayerScore(int newScore)
	{
		playerScore += newScore;
		playerScoreLabel.Text = playerScore.ToString();
	}

	private void GameOverHandler()
	{
		centerText.Play("CenterText");
        centerText.AnimationFinished += CenterText_AnimationFinished;

	}

    private void CenterText_AnimationFinished(StringName animName)
    {
		if (animName == "FadePlayerScoreText")
		{
			GetNode<AnimationPlayer>("MoveTextToCenter").Play("ScaleInGameOverText");
			GetNode<Label>("HBoxContainer/VBoxContainer/GameOver").Visible = true;
		}
    }

    private void CountDownScore()
	{
		startCountDown = true;
	}
}
