internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        if (DataController.BallsController.BallsCount > 0)
            Controller.ShowNewGameScreen(DataController.GameScoreController.TopScores);
        else
        {
            Controller.ShowEndGameScreen(DataController.GameScoreController.TopScores,
                DataController.GameScoreController.CurrentScores);
            DataController.GameScoreController.SaveScores();
            DataController.InputSource.OnRestartPressed += OnRestart;
        }
    }

    private void OnRestart()
    {
        DataController.InputSource.OnRestartPressed -= OnRestart;
        DataController.Dispose();
        Controller.ReloadScene();
    }

    public LoseBallState(GameController controller) : base(controller) { }
}
