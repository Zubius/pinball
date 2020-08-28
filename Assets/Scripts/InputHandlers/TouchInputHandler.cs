using System;
using UnityEngine;

internal class TouchInputHandler : MonoBehaviour, IInputSource
{
    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;
}
