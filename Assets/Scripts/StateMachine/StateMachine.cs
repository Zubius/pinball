using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal sealed class StateMachine
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
        GoToState(GameState.StartGame, null);
    }

    internal void ProcessInput(GameEvent gameEvent, IInputContainer data, IPinballContext context)
    {
        Debug.Log($"Input Event: {gameEvent}, Data: {data}");
        if (_currentState.ProcessEvent(gameEvent, data, context, out GameState nextState))
        {
            GoToState(nextState, context);
        }
    }

    private void GoToState(GameState toState, IPinballContext context)
    {
        if (_states.TryGetValue(toState, out var nextState))
        {
            _currentState?.OnStateExit(context);

            _currentState = nextState;
            Debug.Log($"Enter state: {_currentState.State}");
            _currentState.OnStateEnter(context);
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
