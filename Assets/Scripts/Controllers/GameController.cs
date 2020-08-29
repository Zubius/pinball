using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameController : MonoBehaviour
{
    [SerializeField] private DropBallController dropBallController;
    [SerializeField] private LaunchBallController launchBallController;
    [SerializeField] private ScoreObjectController scoreObjectController;
    [SerializeField] private UIController uiController;
    [SerializeField] private Flipper leftFlipper;
    [SerializeField] private Flipper rightFlipper;

    [SerializeField] private int initBallsCount = 2;

    internal static GameController Instance;

    private StateMachine _stateMachine;
    private Ball _ball;
    private IInputSource _initInputSource;

    private InputSourceType _inputSourceType;

    private void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        _inputSourceType = InputSourceType.Keyboard;
        #elif UNITY_IOS || UNITY_ANDROID
        _inputSourceType = InputSourceType.Touch;
        #endif

        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;

        EnsureComponentExists(dropBallController);
        EnsureComponentExists(launchBallController);
        EnsureComponentExists(scoreObjectController);
        EnsureComponentExists(leftFlipper);
        EnsureComponentExists(rightFlipper);
        EnsureComponentExists(uiController);

        switch (_inputSourceType)
        {
            case InputSourceType.Keyboard:
                _initInputSource = Instantiate(Resources.Load<KeyboardInputHandler>("KeyboardInputHandler"), this.transform.parent);
                break;
            case InputSourceType.Touch:
                var touchEventHandler = Instantiate(Resources.Load<TouchInputHandler>("TouchInputHandler"), this.transform.parent);
                _initInputSource = touchEventHandler;
                uiController.SetStartButtonAction(touchEventHandler.OnStartButtonPressed);
                uiController.SetRestartButtonAction(touchEventHandler.OnRestartGame);
                uiController.SetEventTrigger(Side.Left, FlipperDirection.Up, touchEventHandler.OnLeftPressed);
                uiController.SetEventTrigger(Side.Left, FlipperDirection.Down, touchEventHandler.OnLeftReleased);
                uiController.SetEventTrigger(Side.Right, FlipperDirection.Up, touchEventHandler.OnRightPressed);
                uiController.SetEventTrigger(Side.Right, FlipperDirection.Down, touchEventHandler.OnRightReleased);
                break;
        }

        DataController.Init(_initInputSource, scoreObjectController, initBallsCount);

        _stateMachine = new StateMachine(new Dictionary<GameState, BaseState>(new EnumComparer<GameState>())
            {
                [GameState.StartGame] = new StartGameState(this),
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
        uiController.ShowStartScreenWithTopScores(topScores, _inputSourceType == InputSourceType.Touch);
    }

    internal void PlatNextBall()
    {
        StartGame();
    }

    internal void ShowEndGameScreen(int topScores, int currentScores)
    {
        if (uiController.AIToggleIsON)
        {
            DataController.InputSource = _initInputSource;
        }

        uiController.ShowEndGameScreen(topScores, currentScores, _inputSourceType == InputSourceType.Touch);
    }

    internal void HideStartScreen()
    {
        uiController.HideStartScreen();
    }

    internal void ScoreObject(ScoreObjectType type, int scores)
    {
        scoreObjectController.ScoreObject(type, scores);
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
        _stateMachine.GoToState(GameState.StartGame);
    }

    internal void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    internal void StartGame()
    {
        if (uiController.AIToggleIsON)
        {
            UseAI();
        }
        _stateMachine.GoToState(GameState.LaunchBall);
    }

    private void UseAI()
    {
        if (DataController.InputSource is AIInputHandler ai)
        {
            Destroy(ai.gameObject);
        }

        var aiController = Instantiate(Resources.Load<AIInputHandler>("AIController"), this.transform.parent);
        DataController.InputSource = aiController;
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
