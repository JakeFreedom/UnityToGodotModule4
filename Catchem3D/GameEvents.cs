using Godot;
using System.Runtime.CompilerServices;

public class GameEvents
{

    [Signal]
    public delegate void GameOverEventHandler();

    static GameEvents instance;

    public static GameEvents GetInstance()
    {
        instance = instance ?? (instance = new GameEvents());
        return instance;
    }

}

