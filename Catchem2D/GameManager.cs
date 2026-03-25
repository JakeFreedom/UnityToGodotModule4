using Catchem2D;
using Catchem2D.Events;
using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node3D
{
    [Export] PackedScene obstacleScene;
    [Export] int spawnDelay;
    [Export(PropertyHint.Range, "5,15,1")] int obstaclesToSpawn = 5;

    //Timer spawnTimer;
    static RandomNumberGenerator rng = new RandomNumberGenerator();

    Boolean isGameOver = false;
    AnimationPlayer moveText;
    int playerFinalScore = 0;
    public override void _Ready()
    {
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        //Create a margin around the viewport.
        screenSize.X -= 150;
        screenSize.Y -= 250;
        List<Vector2> spawnPoints = new List<Vector2>();
        bool checkForOverLap = true;
        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            checkForOverLap = true;
            Vector2 spawnPoint = new Vector2(rng.RandiRange(150, (int)screenSize.X), rng.RandiRange(150, (int)screenSize.Y));
            //We only want to add the new spawn point to the list if it's not over lapping... Sprite is 64x64
            while (checkForOverLap)
            {
                if (CheckForOverLap(spawnPoint, spawnPoints))
                    spawnPoint = new Vector2(rng.RandiRange(150, (int)screenSize.X), rng.RandiRange(150, (int)screenSize.Y));
                else
                {
                    checkForOverLap = false;
                    spawnPoints.Add(spawnPoint);
                }
            }

            if(spawnPoints.Count == 0) 
                spawnPoints.Add(spawnPoint);
        }

        //Add them to the scene
        foreach(Vector2 spawnPoint in spawnPoints)
        {
            Obstacles obstacle = obstacleScene.Instantiate<Obstacles>() as Obstacles;
            obstacle.Position = spawnPoint;
            GenerateColor(obstacle);
            GiveRandomZRotation(obstacle);
            AddChild(obstacle);
        }

        GameConfig.Instance.GetBus().Subscribe<BallCaughtEvent>(OnBallCaught);
        GameConfig.Instance.GetBus().Subscribe<GameOverEvent>(OnGameOver);
        moveText = GetNode<AnimationPlayer>("../CanvasLayer/MoveTextPlayer");
    }

    double countDownDelay = 0;
    public override void _Process(double delta)
    {
        if(isGameOver && playerFinalScore >= 0)
        {
            if (countDownDelay > .3)
            {
                countDownDelay = 0;
                GetNode<Label>("../CanvasLayer/HBoxContainer/PlayerScore").Text = playerFinalScore--.ToString();
            }

            countDownDelay += delta;
            GD.Print(playerFinalScore);
        }
    }

    private void OnGameOver(GameOverEvent gameOverEvent) {

        //GD.Print("Game Over, Move Text to the center of the screen");
        //get the text container and move it to the center of the screen
        moveText.Play("MoveTextToCenterOfScreen");
        moveText.AnimationFinished += MoveText_AnimationFinished;
        playerFinalScore = Int32.Parse(GetNode<Label>("../CanvasLayer/HBoxContainer/PlayerScore").Text);
        
    }

    private void MoveText_AnimationFinished(StringName animName)
    {
        //GetTree().Paused = true;
        //Get the score and count it down
        isGameOver = true;
    }

    public void OnBallCaught(BallCaughtEvent evt)
    {
        GD.Print($"Game Manager --- Ball caught {evt.BallName}");
    }

    /// <summary>
    /// returns true if there is overlap
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="spawnPoints"></param>
    /// <returns></returns>
    private bool CheckForOverLap(Vector2 spawnPoint, List<Vector2> spawnPoints) {
        foreach (Vector2 goodPoints in spawnPoints)
            if (spawnPoint.X >= goodPoints.X && spawnPoint.X+64 <= goodPoints.X + 64)
                return true;
            else
                return  false;

            //If something goes wrong we will error on the side that there is overlap and return false.
            return false;
    }
    private void GenerateColor(Obstacles obj) => obj.SetColor(new Vector3(rng.RandfRange(0, 1), rng.RandfRange(0, 1), rng.RandfRange(0, 1)));
       private void GiveRandomZRotation(Obstacles obj) => obj.Rotate(Mathf.DegToRad(rng.RandiRange(-15, 15)));

    #region Static Methods
    public static int GetRandI(int min, int max)
    {
        return rng.RandiRange(min, max);
    }
    public static float GetRandf(float min, float max)
    {
        return rng.RandfRange(min, max);
    }
    #endregion
}
