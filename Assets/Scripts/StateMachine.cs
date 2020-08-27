using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class StateMachine
{
    private readonly Dictionary<GameState, BaseState> _states;
    private BaseState _currentState;

    internal StateMachine(Dictionary<GameState, BaseState> states)
    {
        _states = states.ToDictionary(
            s => s.Key,
            s => s.Value,
            new EnumComparer<GameState>());
    }

    internal void GoToState(GameState toState)
    {
        if (_states.TryGetValue(toState, out var nextState))
        {
            _currentState?.OnStateExit();

            _currentState = nextState;
            _currentState.OnStateEnter();
        }
        else
        {
            Debug.LogError($"Unexpected {nameof(GameState)}: {toState.ToString()}");
        }
    }
}

internal class EnumComparer<TEnum> : IEqualityComparer<TEnum> where TEnum : Enum
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
