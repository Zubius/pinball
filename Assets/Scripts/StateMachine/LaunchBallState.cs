using UnityEngine;

internal class LaunchBallState : BaseGameState
{
    internal override GameState State => GameState.LaunchBall;

    private readonly BallType[] _ballTypes = {BallType.Simple};

    internal override void OnStateEnter()
    {
        GameController.Instance.UpdateBallsLeft(DataController.BallsController.BallsCount);
        GameController.Instance.HideStartScreen();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, out GameState nextState)
    {
        if (gameEvent == GameEvent.LaunchBall && data is LaunchInput launchData)
        {
            DataController.BallsController.DecrementBallsCount();
            GameController.Instance.UpdateBallsLeft(DataController.BallsController.BallsCount);
            GameController.Instance.LaunchBall(_ballTypes[Random.Range(0, _ballTypes.Length - 1)], launchData.HoldDuration);

            nextState = GameState.GameProcess;
            return true;
        }

        nextState = GameState.None;
        return false;
    }
}
