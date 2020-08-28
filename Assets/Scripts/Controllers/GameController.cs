using System.Collections.Generic;
using UnityEngine;

internal class GameController : MonoBehaviour
{
    [SerializeField] private DropBallController dropBallController;
    [SerializeField] private LaunchBallController launchBallController;
    [SerializeField] private UIController uiController;
    [SerializeField] private Flipper leftFlipper;
    [SerializeField] private Flipper rightFlipper;

    [SerializeField] private int initBallsCount = 2;

    internal static GameController Instance;

    internal GameScoreController GameScoreController;
    internal IInputSource InputSource;
    internal BallsController BallsController;
    internal ScoreObjectController ScoreObjectController;

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

        GameScoreController = new GameScoreController();
        BallsController = new BallsController(initBallsCount);
        ScoreObjectController = new ScoreObjectController();

        EnsureComponentExists(dropBallController);
        EnsureComponentExists(launchBallController);
        EnsureComponentExists(leftFlipper);
        EnsureComponentExists(rightFlipper);
        EnsureComponentExists(uiController);

        switch (_inputSourceTypeType)
        {
            case InputSourceType.Keyboard:
                InputSource = Instantiate(Resources.Load<KeyboardInputHandler>("KeyboardInputHandler"), this.transform.parent);
                break;
            case InputSourceType.Touch:
                InputSource = Instantiate(Resources.Load<TouchInputHandler>("TouchInputHandler"), this.transform.parent);
                var touchEventHandler = Instantiate(Resources.Load<TouchInputHandler>("TouchInputHandler"), this.transform.parent);
                InputSource = touchEventHandler;
                uiController.SetStartButtonAction(touchEventHandler.OnStartButtonPressed);
                uiController.SetEventTrigger(Side.Left, FlipperDirection.Up, touchEventHandler.OnLeftReleased);
                uiController.SetEventTrigger(Side.Left, FlipperDirection.Down, touchEventHandler.OnLeftPressed);
                uiController.SetEventTrigger(Side.Right, FlipperDirection.Up, touchEventHandler.OnRightReleased);
                uiController.SetEventTrigger(Side.Right, FlipperDirection.Down, touchEventHandler.OnRightPressed);
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

    internal void ShowNewGameScreen(int topScores)
    {
        uiController.ShowStartScreenWithTopScores(topScores);
    }

    internal void HideStartScreen()
    {
        uiController.HideStartScreen();
    }

    internal void LaunchBall(BallType type, float force)
    {
        launchBallController.LaunchBall(type, force);
    }

    internal void UpdateScores(int scores)
    {
        uiController.SetCurrentScores(scores);
    }

    internal void UpdateBallsLeft(int count)
    {
        uiController.SetBallsLeft(count);
    }

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
        GameScoreController.SaveScores();
        GameScoreController.Reset();
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
