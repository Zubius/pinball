
public class LoseBallState : BaseState
{
    public override GameState State => GameState.LoseBall;

    public override void OnStateEnter()
    {
        GameController.Instance.ShowNewGameScreen();
    }
}
