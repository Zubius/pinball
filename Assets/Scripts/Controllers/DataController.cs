internal static class DataController
{
    internal static GameScoreController GameScoreController;
    internal static IInputSource InputSource;
    internal static BallsController BallsController;
    internal static ScoreObjectController ScoreObjectController;

    internal static void Init(IInputSource inputSource, ScoreObjectController scoreObjectController, int initBallsCount)
    {
        InputSource = inputSource;
        GameScoreController = new GameScoreController();
        BallsController = new BallsController(initBallsCount);
        ScoreObjectController = scoreObjectController;
    }

    internal static void Dispose()
    {
        InputSource = null;
        GameScoreController = null;
        BallsController = null;
        ScoreObjectController = null;
    }
}
