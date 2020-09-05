using CoreHapticsUnity;

internal sealed class StartGameState : BaseGameState
{
    internal override GameState State => GameState.StartGame;

    internal override void OnStateEnter(IPinballContext context)
    {
        context.SetNewGame();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, IPinballContext context, out GameState nextState)
    {
        if (gameEvent == GameEvent.StartGame)
        {
            context.PrepareLaunchBall();
            nextState = GameState.LaunchBall;
            return true;
        }

        nextState = GameState.None;
        return false;
    }
}
