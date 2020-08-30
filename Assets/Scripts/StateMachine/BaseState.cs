using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class BaseAbstractState
{
    protected readonly GameController Controller;

    internal BaseAbstractState(GameController controller)
    {
        Controller = controller;
    }

    internal abstract GameState State { get; }

    internal virtual void OnStateEnter() {}


    internal virtual void OnStateExit() {}
}

internal enum GameState
{
    StartGame,
    LaunchBall,
    GameProcess,
    LoseBall,
    EndGame,
}
