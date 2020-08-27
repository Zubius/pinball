using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private StateMachine _stateMachine;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _stateMachine = new StateMachine(new Dictionary<GameState, BaseState>(new EnumComparer<GameState>())
            {
                [GameState.LaunchBall] = new LaunchBallState(),
                [GameState.GameProcess] = new GameProcessState(),
                [GameState.LoseBall] = new LoseBallState(),
            }
        );
    }
}
