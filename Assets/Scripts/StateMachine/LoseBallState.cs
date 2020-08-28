internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        Controller.ShowNewGameScreen(Controller.GameScoreController.TopScores);
    }

    public LoseBallState(GameController controller) : base(controller) { }
}
