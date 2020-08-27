
public class LaunchBallState : BaseState
{
    public override GameState State => GameState.LaunchBall;

    public override void OnStateExit()
    {
        GameController.Instance.LaunchBall(42);
    }
}
