using System;
using UnityEngine;

internal class TouchInputHandler : MonoBehaviour, IInputSource
{
    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;

    internal void OnStartButtonPressed()
    {
        OnStartPressed?.Invoke();
    }

    internal void OnLeftPressed()
    {
        OnFlipperAction?.Invoke(Side.Left, FlipperDirection.Down);
    }

    internal void OnLeftReleased()
    {
        OnFlipperAction?.Invoke(Side.Left, FlipperDirection.Up);
    }

    internal void OnRightPressed()
    {
        OnFlipperAction?.Invoke(Side.Right, FlipperDirection.Down);
    }

    internal void OnRightReleased()
    {
        OnFlipperAction?.Invoke(Side.Right, FlipperDirection.Up);
    }
}
