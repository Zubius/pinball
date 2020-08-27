using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameController : MonoBehaviour
{
    internal static GameController Instance;

    internal ScoreController ScoreController;

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

        ScoreController = new ScoreController();

        Init();
    }

    internal void ShowNewGameScreen() {}

    internal void LaunchBall(float force)
    {
        _stateMachine.GoToState(GameState.GameProcess);
    }

    internal void MoveLeftFlipper() {}

    internal void MoveRightFlipper() {}

    internal void UpdateScores() {}

    private void Init()
    {
        OnBallDropped();
        ScoreController.SaveScores();
        ScoreController.Reset();
    }

    private void OnBallDropped()
    {
        _stateMachine.GoToState(GameState.LoseBall);
    }

    private void OnStartPressed()
    {
        _stateMachine.GoToState(GameState.LaunchBall);
    }
}

internal enum Side
{
    Left, Right
}
