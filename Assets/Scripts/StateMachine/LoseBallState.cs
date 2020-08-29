internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        if (DataController.BallsController.BallsCount > 0)
            Controller.PlatNextBall();
        else
        {
            DataController.GameScoreController.SaveScores();
            Controller.ShowEndGameScreen(DataController.GameScoreController.TopScores,
                DataController.GameScoreController.CurrentScores);
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
