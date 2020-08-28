using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameController : MonoBehaviour
{
    [SerializeField] private DropBallController dropBallController;
    [SerializeField] private LaunchBallController launchBallController;

    internal static GameController Instance;

    internal ScoreController ScoreController;

    private StateMachine _stateMachine;
    private Ball _ball;

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

        EnsureComponentExists(dropBallController);
        EnsureComponentExists(launchBallController);

        dropBallController.OnBallDropped += OnBallDropped;
        launchBallController.OnBallLaunched += OnBallLaunched;

        Init();
    }

    internal void ShowNewGameScreen() {}

    internal void LaunchBall(float force)
    {
        launchBallController.LaunchBall(force);
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
        if (_ball != null)
        {
            Destroy(_ball.gameObject);
        }

        _stateMachine.GoToState(GameState.LoseBall);

        //TODO debug
        OnStartPressed();
    }

    private void OnBallLaunched(Ball ball)
    {
        _ball = ball;
        _stateMachine.GoToState(GameState.GameProcess);
    }

    private void OnStartPressed()
    {
        _stateMachine.GoToState(GameState.LaunchBall);
    }

    private void EnsureComponentExists<T>(T component) where T : MonoBehaviour
    {
        if (component == null)
        {
            component = FindObjectOfType<T>();
            if (component == null)
            {
                Debug.LogError($"No {nameof(T)} Found!");
            }
        }
    }
}

internal enum Side
{
    Left, Right
}
