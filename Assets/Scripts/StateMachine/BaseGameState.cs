internal abstract class BaseGameState : BaseAbstractState<GameState, IInputContainer, GameEvent>
{
    internal abstract override GameState State { get; }

    internal abstract override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, out GameState nextState);
}
