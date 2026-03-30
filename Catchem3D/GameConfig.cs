using Godot;
using System;

public partial class GameConfig : Node3D
{


    [Signal]
    public delegate void GameOverEventHandler();

    static bool isGameOver = false;
    public GameConfig() {

        Instance = this;
    }


    public static GameConfig Instance { get; private set; }
    public static bool IsGameOver => isGameOver;
    public static void EmitGameOver()
    {
        Instance.EmitSignalGameOver();
        isGameOver = true;
    }
}
