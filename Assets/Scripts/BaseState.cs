using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public virtual GameState State { get; }

    public virtual void OnStateEnter() {}

    public virtual void HandleInput() {}

    public virtual void OnStateExit() {}
}

public enum GameState
{
    None = 0,
    LaunchBall,
    GameProcess,
    LoseBall,
}
