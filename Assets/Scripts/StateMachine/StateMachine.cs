using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class StateMachine
{
    private readonly Dictionary<GameState, BaseGameState> _states;
    private BaseGameState _currentState;

    internal StateMachine(Dictionary<GameState, BaseGameState> states)
    {
        _states = states.ToDictionary(
            s => s.Key,
            s => s.Value,
            new EnumComparer<GameState>());
    }

    internal void Start()
    {
        GoToState(GameState.StartGame);
    }

    internal void ProcessInput(GameEvent gameEvent, IInputContainer data)
    {
        Debug.Log($"Input Event: {gameEvent}, Data: {data}");
        if (_currentState.ProcessEvent(gameEvent, data, out GameState nextState))
        {
            GoToState(nextState);
        }
    }

    private void GoToState(GameState toState)
    {
        if (_states.TryGetValue(toState, out var nextState))
        {
            _currentState?.OnStateExit();

            _currentState = nextState;
            Debug.Log($"Enter state: {_currentState.State}");
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
