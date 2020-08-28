
internal class LaunchBallState : BaseState
{
    internal override GameState State => GameState.LaunchBall;

    internal override void OnStateEnter()
    {
        Controller.InputSource.OnLaunchReleased += OnLaunchBallReleased;
    }

    internal override void OnStateExit()
    {
        Controller.InputSource.OnLaunchReleased -= OnLaunchBallReleased;
    }

    public LaunchBallState(GameController controller) : base(controller) { }

    private void OnLaunchBallReleased(float duration)
    {
        if (duration > 0)
            Controller.LaunchBall(duration);
    }
}
