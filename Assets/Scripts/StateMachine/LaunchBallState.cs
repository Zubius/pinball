using UnityEngine;

internal class LaunchBallState : BaseState
{
    internal override GameState State => GameState.LaunchBall;

    private readonly BallType[] _ballTypes = {BallType.Simple};

    internal override void OnStateEnter()
    {
        DataController.InputSource.OnLaunchReleased += OnLaunchBallReleased;
        DataController.BallsController.DecrementBallsCount();
        Controller.UpdateBallsLeft(DataController.BallsController.BallsCount);
        Controller.HideStartScreen();
    }

    internal override void OnStateExit()
    {
        DataController.InputSource.OnLaunchReleased -= OnLaunchBallReleased;
    }

    public LaunchBallState(GameController controller) : base(controller) { }

    private void OnLaunchBallReleased(float duration)
    {
        if (duration > 0)
        {
            Controller.LaunchBall(_ballTypes[Random.Range(0, _ballTypes.Length - 1)], duration);
        }
    }
}
