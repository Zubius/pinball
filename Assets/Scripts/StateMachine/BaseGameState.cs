internal abstract class BaseGameState : BaseAbstractState<GameState, IInputContainer, IPinballContext, GameEvent>
{
    internal abstract override GameState State { get; }

    internal abstract override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, IPinballContext context, out GameState nextState);
}
