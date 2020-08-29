internal static class DataController
{
    internal static GameScoreController GameScoreController;
    internal static IInputSource InputSource;
    internal static BallsController BallsController;
    internal static ScoreTaskController ScoreTaskController;
    internal static ScoreObjectController ScoreObjectController;

    internal static void Init(IInputSource inputSource, ScoreObjectController scoreObjectController, int initBallsCount)
    {
        InputSource = inputSource;
        GameScoreController = new GameScoreController();
        ScoreTaskController = new ScoreTaskController();
        BallsController = new BallsController(initBallsCount);
        ScoreObjectController = scoreObjectController;
    }

    internal static void Dispose()
    {
        InputSource = null;
        GameScoreController = null;
        ScoreTaskController = null;
        BallsController = null;
        ScoreObjectController = null;
    }
}
