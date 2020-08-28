using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameController : MonoBehaviour
{
    [SerializeField] private DropBallController dropBallController;
    [SerializeField] private LaunchBallController launchBallController;
    [SerializeField] private Flipper leftFlipper;
    [SerializeField] private Flipper rightFlipper;

    internal static GameController Instance;

    internal ScoreController ScoreController;
    internal IInputSource InputSource;

    private StateMachine _stateMachine;
    private Ball _ball;

    private InputSourceType _inputSourceTypeType;

    private void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        _inputSourceTypeType = InputSourceType.Keyboard;
        #elif UNITY_IOS || UNITY_ANDROID
        _inputSourceTypeType = InputSourceType.Touch;
        #endif

        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;

        ScoreController = new ScoreController();

        EnsureComponentExists(dropBallController);
        EnsureComponentExists(launchBallController);
        EnsureComponentExists(leftFlipper);
        EnsureComponentExists(rightFlipper);

        switch (_inputSourceTypeType)
        {
            case InputSourceType.Keyboard:
                InputSource = Instantiate(Resources.Load<KeyboardInputHandler>("KeyboardInputHandler"), this.transform.parent);
                break;
            case InputSourceType.Touch:
                InputSource = Instantiate(Resources.Load<TouchInputHandler>("TouchInputHandler"), this.transform.parent);
                break;
            case InputSourceType.AI:
                InputSource = new AIInputHandler();
                break;
        }

        InputSource.OnStartPressed += OnStartPressed;

        _stateMachine = new StateMachine(new Dictionary<GameState, BaseState>(new EnumComparer<GameState>())
            {
                [GameState.LaunchBall] = new LaunchBallState(this),
                [GameState.GameProcess] = new GameProcessState(this),
                [GameState.LoseBall] = new LoseBallState(this),
            }
        );

        dropBallController.OnBallDropped += OnBallDropped;
        launchBallController.OnBallLaunched += OnBallLaunched;

        Init();
    }

    internal void ShowNewGameScreen() {}

    internal void LaunchBall(float force)
    {
        launchBallController.LaunchBall(force);
    }

    internal void UpdateScores() {}

    internal void MoveLeftFlipper(FlipperDirection direction)
    {
        MoveFlipper(leftFlipper, direction);
    }

    internal void MoveRightFlipper(FlipperDirection direction)
    {
        MoveFlipper(rightFlipper, direction);
    }

    private void MoveFlipper(Flipper flipper, FlipperDirection direction)
    {
        switch (direction)
        {
            case FlipperDirection.Up:
                flipper.Up();
                break;
            case FlipperDirection.Down:
                flipper.Down();
                break;
        }
    }

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
    None = 0,
    Left,
    Right
}
