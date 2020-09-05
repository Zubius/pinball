internal sealed class EndGameState : BaseGameState
{
    internal override void OnStateEnter(IPinballContext context)
    {
        context.SetEndGame();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, IPinballContext context, out GameState nextState)
    {
        if (gameEvent == GameEvent.Restart)
        {
            context.Restart();
        }

        nextState = GameState.None;
        return false;
    }

    internal override GameState State => GameState.EndGame;
}
