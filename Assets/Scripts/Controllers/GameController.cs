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
    private AIInputHandler _aiController;
    private InputSourceController _inputSourceController;

    private InputSourceType _inputSourceType;

    private void Start()
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

        DataController.Init(scoreObjectController, initBallsCount);
        DataController.GameScoreController.OnScoreChanged += OnGameScoreChanged;
        DataController.ScoreObjectController.OnScored += OnScored;

        _stateMachine = new StateMachine(new Dictionary<GameState, BaseGameState>(new EnumComparer<GameState>())
            {
                [GameState.StartGame] = new StartGameState(),
                [GameState.LaunchBall] = new LaunchBallState(),
                [GameState.GameProcess] = new GameProcessState(),
                [GameState.LoseBall] = new LoseBallState(),
                [GameState.EndGame] = new EndGameState()
            }
        );

        dropBallController.OnBallDropped += OnBallDropped;
        launchBallController.OnBallLaunched += OnBallLaunched;

        Init();
        InitInputSourceController(_initInputSource);
    }

    private void OnScored(ScoreObjectArgs obj)
    {
        _stateMachine.ProcessInput(GameEvent.AddScores, new ScoreInput(obj));
    }

    private void OnGameScoreChanged()
    {
        _stateMachine.ProcessInput(GameEvent.UpdateScores, null);
    }

    internal void PlatNextBall()
    {
        StartGame();
    }

    internal void SetBallDropped()
    {
        _stateMachine.ProcessInput(GameEvent.HandleBallDropped, null);
    }

    internal void SetEndGame()
    {
        if (uiController.AIToggleIsON)
        {
            _inputSourceController.SetSingleSource(_initInputSource);
        }
    }

    internal void ScoreObject(int id, int scores, int? taskId)
    {
        scoreObjectController.ScoreObject(id, scores, taskId);

    }

    internal void LaunchBall(BallType type, float force)
    {
        launchBallController.LaunchBall(type, force);
    }

    #region update ui
    internal void ShowNewGameScreen(int topScores)
    {
        uiController.ShowStartScreenWithTopScores(topScores, _inputSourceType == InputSourceType.Touch);
    }

    internal void ShowEndGameScreen(int topScores, int currentScores)
    {
        uiController.ShowEndGameScreen(topScores, currentScores, _inputSourceType == InputSourceType.Touch);
    }

    internal void HideStartScreen()
    {
        uiController.HideStartScreen();
    }

    internal void UpdateScores(int scores)
    {
        uiController.SetCurrentScores(scores);
    }

    internal void UpdateBallsLeft(int count)
    {
        uiController.SetBallsLeft(count);
    }
    #endregion

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
        _stateMachine.Start();
    }

    #region InputSource
    private void InitInputSourceController(IInputSource source)
    {
        _inputSourceController = new InputSourceController();
        _inputSourceController.AddSource(source);
        _inputSourceController.OnStartPressed += InputSourceControllerOnOnStartPressed;
        _inputSourceController.OnLaunchReleased += InputSourceControllerOnOnLaunchReleased;
        _inputSourceController.OnFlipperAction += InputSourceControllerOnOnFlipperAction;
        _inputSourceController.OnRestartPressed += InputSourceControllerOnOnRestartPressed;
    }

    private void InputSourceControllerOnOnRestartPressed()
    {
        _stateMachine.ProcessInput(GameEvent.Restart, null);
    }

    private void InputSourceControllerOnOnFlipperAction(Side arg1, FlipperDirection arg2)
    {
        _stateMachine.ProcessInput(GameEvent.MoveFlipper, new FlipperInput(arg1, arg2));
    }

    private void InputSourceControllerOnOnLaunchReleased(float obj)
    {
        _stateMachine.ProcessInput(GameEvent.LaunchBall, new LaunchInput(obj));
    }

    private void InputSourceControllerOnOnStartPressed()
    {
        _stateMachine.ProcessInput(GameEvent.StartGame, null);
    }

    #endregion

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

        _stateMachine.ProcessInput(GameEvent.DropBall, null);
    }

    private void OnBallLaunched(Ball ball)
    {
        _ball = ball;
    }

    internal void StartGame()
    {
        if (uiController.AIToggleIsON)
        {
            UseAI();
        }
    }

    private void UseAI()
    {
        if (_aiController != null)
        {
            _aiController.Restart();
        }
        else
        {
            _aiController = Instantiate(Resources.Load<AIInputHandler>("AIController"), this.transform.parent);
            _inputSourceController.SetSingleSource(_aiController);
        }
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
