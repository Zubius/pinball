using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BaseState
{
    internal virtual GameState State { get; }

    internal virtual void OnStateEnter() {}

    internal virtual void HandleInput() {}

    internal virtual void OnStateExit() {}
}

internal enum GameState
{
    LaunchBall,
    GameProcess,
    LoseBall,
}
