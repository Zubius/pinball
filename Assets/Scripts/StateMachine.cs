
using System;
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
            new EnumComparer<GameState>());
    }
}

public class EnumComparer<TEnum> : IEqualityComparer<TEnum> where TEnum : Enum
{
    public bool Equals(TEnum x, TEnum y)
    {
        return EqualityComparer<TEnum>.Default.Equals(x, y);
    }

    public int GetHashCode(TEnum obj)
    {
        return EqualityComparer<TEnum>.Default.GetHashCode(obj);
    }
}
