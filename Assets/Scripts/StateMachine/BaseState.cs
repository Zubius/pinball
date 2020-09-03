using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class BaseAbstractState<TState, TData, TEvent> where TState : Enum where TData : IInputContainer where TEvent : Enum
{
    internal abstract TState State { get; }

    internal virtual void OnStateEnter() {}

    internal abstract bool ProcessEvent(TEvent gameEvent, TData data,  out TState nextState);

    internal virtual void OnStateExit() {}
}

internal enum GameState
{
    None = 0,
    StartGame,
    LaunchBall,
    GameProcess,
    LoseBall,
    EndGame,
}

internal enum GameEvent
{
    None = 0,
    StartGame,
    LaunchBall,
    MoveFlipper,
    DropBall,
    UpdateScores,
    AddScores,
    HandleBallDropped,
    Restart
}
