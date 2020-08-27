internal class LoseBallState : BaseState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        GameController.Instance.ShowNewGameScreen();
    }
}
