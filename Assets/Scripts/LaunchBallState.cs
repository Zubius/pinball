
internal class LaunchBallState : BaseState
{
    internal override GameState State => GameState.LaunchBall;

    internal override void OnStateExit()
    {
        GameController.Instance.LaunchBall(1);
    }
}
