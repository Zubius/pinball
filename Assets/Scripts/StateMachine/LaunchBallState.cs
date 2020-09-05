using UnityEngine;

internal sealed class LaunchBallState : BaseGameState
{
    internal override GameState State => GameState.LaunchBall;

    private readonly BallType[] _ballTypes = {BallType.Simple};

    internal override void OnStateEnter(IPinballContext context)
    {
        context.SetNewRound();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, IPinballContext context, out GameState nextState)
    {
        if (gameEvent == GameEvent.LaunchBall && data is LaunchInput launchData)
        {
            context.LaunchBall(_ballTypes[Random.Range(0, _ballTypes.Length - 1)], launchData.HoldDuration);

            nextState = GameState.GameProcess;
            return true;
        }

        nextState = GameState.None;
        return false;
    }
}
