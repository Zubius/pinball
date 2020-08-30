internal class LoseBallState : BaseAbstractState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        if (DataController.BallsController.BallsCount > 0)
            Controller.PlatNextBall();
        else
        {
            Controller.SetEndGame();
        }
    }

    public LoseBallState(GameController controller) : base(controller) { }
}
