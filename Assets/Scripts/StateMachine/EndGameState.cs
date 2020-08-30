internal class EndGameState : BaseAbstractState
{
    public EndGameState(GameController controller) : base(controller) { }

    internal override void OnStateEnter()
    {
        DataController.GameScoreController.SaveScores();
        Controller.ShowEndGameScreen(DataController.GameScoreController.TopScores,
            DataController.GameScoreController.CurrentScores);
        DataController.InputSource.OnRestartPressed += OnRestart;
    }

    private void OnRestart()
    {
        DataController.InputSource.OnRestartPressed -= OnRestart;
        DataController.Dispose();
        Controller.ReloadScene();
    }

    internal override GameState State => GameState.EndGame;
}
