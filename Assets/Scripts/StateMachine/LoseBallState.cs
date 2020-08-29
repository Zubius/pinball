internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        if (Controller.BallsController.BallsCount > 0)
            Controller.ShowNewGameScreen(Controller.GameScoreController.TopScores);
        else
            Controller.ShowEndGameScreen(Controller.GameScoreController.TopScores, Controller.GameScoreController.CurrentScores);
    }

    public LoseBallState(GameController controller) : base(controller) { }
}
