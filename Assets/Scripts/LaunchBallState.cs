
internal class LaunchBallState : BaseState
{
    internal override GameState State => GameState.LaunchBall;

    internal override void OnStateEnter()
    {
        GameController.Instance.LaunchBall(1);
    }
}
