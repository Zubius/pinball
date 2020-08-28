internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        Controller.ShowNewGameScreen();
    }

    public LoseBallState(GameController controller) : base(controller) { }
}
