using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class BaseState
{
    protected readonly GameController Controller;
    
    internal BaseState(GameController controller)
    {
        Controller = controller;
    }

    internal virtual GameState State { get; }

    internal virtual void OnStateEnter() {}


    internal virtual void OnStateExit() {}
}

internal enum GameState
{
    LaunchBall,
    GameProcess,
    LoseBall,
}
