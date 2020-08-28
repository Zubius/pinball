using System;
using UnityEngine;

internal interface IInputSource
{
    event Action<float> OnLaunchReleased;
    event Action<Side, FlipperDirection> OnFlipperAction;
    event Action OnStartPressed;
}

internal enum InputSourceType
{
    Keyboard = 0,
    Touch = 1,
    AI = 2
}

internal enum FlipperDirection
{
    Up,
    Down
}
