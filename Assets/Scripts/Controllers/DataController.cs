internal static class DataController
{
    internal static GameScoreController GameScoreController;
    internal static BallsController BallsController;
    internal static ScoreTaskController ScoreTaskController;
    internal static ScoreObjectController ScoreObjectController;

    internal static void Init(ScoreObjectController scoreObjectController, int initBallsCount)
    {
        GameScoreController = new GameScoreController();
        ScoreTaskController = new ScoreTaskController();
        BallsController = new BallsController(initBallsCount);
        ScoreObjectController = scoreObjectController;
    }

    internal static void Dispose()
    {
        GameScoreController = null;
        ScoreTaskController = null;
        BallsController = null;
        ScoreObjectController = null;
    }

    internal static void CreateAndAssignTask(int reward, ScoreObjectType type)
    {
        var grouped = ScoreObjectController.GetObjectIdsByType(ScoreObjectType.Grouped);
        var groupTask = ScoreTaskController.CreateNewTas(500, grouped, ScoreObjectType.Grouped);

        ScoreTaskController.AddNewTask(groupTask);
        ScoreObjectController.AssignTaskToObjects(groupTask, grouped);
    }
}
