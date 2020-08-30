internal class StartGameState : BaseState
{
    internal override GameState State => GameState.StartGame;

    internal override void OnStateEnter()
    {
        Controller.ShowNewGameScreen(DataController.GameScoreController.TopScores);
        DataController.InputSource.OnStartPressed += Controller.StartGame;

        var taskId = DataController.ScoreTaskController.GetNextTaskId();
        var grouped = DataController.ScoreObjectController.GetObjectIdsByType(ScoreObjectType.Grouped);
        var groupTask = new GroupScoreTask(taskId, grouped, 500, ScoreObjectType.Grouped);

        DataController.ScoreTaskController.AddNewTask(groupTask);
        DataController.ScoreObjectController.AssignTaskToObjects(groupTask, grouped);
    }

    internal override void OnStateExit()
    {
        DataController.InputSource.OnStartPressed -= Controller.StartGame;
    }

    public StartGameState(GameController controller) : base(controller) { }
}
