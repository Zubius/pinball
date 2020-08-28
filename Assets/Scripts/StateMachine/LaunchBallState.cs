using UnityEngine;

internal class LaunchBallState : BaseState
{
    internal override GameState State => GameState.LaunchBall;

    private readonly BallType[] _ballTypes = {BallType.Simple};

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
        {
            Controller.BallsController.DecrementBallsCount();
            Controller.LaunchBall(_ballTypes[Random.Range(0, _ballTypes.Length - 1)], duration);
        }
    }
}
