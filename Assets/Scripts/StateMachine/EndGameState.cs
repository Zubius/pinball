internal class EndGameState : BaseGameState
{
    internal override void OnStateEnter()
    {
        DataController.GameScoreController.SaveScores();
        GameController.Instance.ShowEndGameScreen(DataController.GameScoreController.TopScores,
            DataController.GameScoreController.CurrentScores);
        GameController.Instance.SetEndGame();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, out GameState nextState)
    {
        if (gameEvent == GameEvent.Restart)
        {
            Restart();
        }

        nextState = GameState.None;
        return false;
    }

    private void Restart()
    {
        DataController.Dispose();
        GameController.Instance.ReloadScene();
    }

    internal override GameState State => GameState.EndGame;
}
