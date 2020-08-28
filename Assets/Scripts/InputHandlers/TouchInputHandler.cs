using System;
using UnityEngine;

internal class TouchInputHandler : MonoBehaviour, IInputSource
{
    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;

    private float _pressedTime = 0;
    private int _holdTouchesCount = 0;

    internal void OnStartButtonPressed()
    {
        OnStartPressed?.Invoke();
    }

    internal void OnLeftPressed()
    {
        OnFlipperAction?.Invoke(Side.Left, FlipperDirection.Up);

        StartCountHold();
    }

    internal void OnLeftReleased()
    {
        OnFlipperAction?.Invoke(Side.Left, FlipperDirection.Down);

        StopCountHold();
    }

    internal void OnRightPressed()
    {
        OnFlipperAction?.Invoke(Side.Right, FlipperDirection.Up);

        StartCountHold();
    }

    internal void OnRightReleased()
    {
        OnFlipperAction?.Invoke(Side.Right, FlipperDirection.Down);

        StopCountHold();
    }

    private void StartCountHold()
    {
        if (_pressedTime == 0)
            _pressedTime = Time.time;

        _holdTouchesCount++;
    }

    private void StopCountHold()
    {
        _holdTouchesCount--;

        if (_holdTouchesCount == 0)
        {
            OnLaunchReleased?.Invoke(Time.time - _pressedTime);
            _pressedTime = 0;
        }
    }
}
