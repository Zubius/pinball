using CoreHapticsUnity;

internal class StartGameState : BaseGameState
{
    internal override GameState State => GameState.StartGame;

    internal override void OnStateEnter()
    {
        CoreHapticsUnityProxy.LogLevel = LogsLevel.None;

        GameController.Instance.ShowNewGameScreen(DataController.GameScoreController.TopScores);

        DataController.CreateAndAssignTask(500, ScoreObjectType.Grouped);
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
