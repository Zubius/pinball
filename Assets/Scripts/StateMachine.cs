
using System.Collections.Generic;
using System.Linq;

public class StateMachine
{
    private Dictionary<GameState, BaseState> _states;

    public StateMachine(Dictionary<GameState, BaseState> states)
    {
        _states = states.ToDictionary(
            s => s.Key,
            s => s.Value,
            new GameStateComparer());
    }
}

public class GameStateComparer : IEqualityComparer<GameState> {
    public bool Equals(GameState x, GameState y) {
        return x == y;
    }

    public int GetHashCode(GameState x) {
        return (int)x;
    }
}
