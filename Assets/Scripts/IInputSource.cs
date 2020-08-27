using System;

internal interface IInputSource
{
    event Action<Side, float> OnGameInput;
}
