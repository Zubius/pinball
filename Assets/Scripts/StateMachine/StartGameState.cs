using CoreHapticsUnity;

internal class StartGameState : BaseGameState
{
    internal override GameState State => GameState.StartGame;

    internal override void OnStateEnter()
    {
        CoreHapticsUnityProxy.LogLevel = LogsLevel.None;

        GameController.Instance.ShowNewGameScreen(DataController.GameScoreController.TopScores);

        var taskId = DataController.ScoreTaskController.GetNextTaskId();
        var grouped = DataController.ScoreObjectController.GetObjectIdsByType(ScoreObjectType.Grouped);
        var groupTask = new GroupScoreTask(taskId, grouped, 500, ScoreObjectType.Grouped);

        DataController.ScoreTaskController.AddNewTask(groupTask);
        DataController.ScoreObjectController.AssignTaskToObjects(groupTask, grouped);
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, out GameState nextState)
    {
        if (gameEvent == GameEvent.StartGame)
        {
            GameController.Instance.StartGame();
            nextState = GameState.LaunchBall;
            return true;
        }

        nextState = GameState.None;
        return false;
    }
}
