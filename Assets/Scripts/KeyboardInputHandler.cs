using System;
using UnityEngine;

internal class KeyboardInputHandler : MonoBehaviour, IInputSource
{
    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;
    

    private float _holdStartTime = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _holdStartTime = Time.time;
        if (Input.GetKeyUp(KeyCode.Space) && _holdStartTime > 0)
        {
            OnLaunchReleased?.Invoke(Time.time - _holdStartTime);
            _holdStartTime = 0;
        }

        if (_holdStartTime == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
                InvokeFlipperAction(Side.Left, FlipperDirection.Up);
            else if (Input.GetKeyUp(KeyCode.A))
                InvokeFlipperAction(Side.Left, FlipperDirection.Down);
            if (Input.GetKeyDown(KeyCode.D))
                InvokeFlipperAction(Side.Right, FlipperDirection.Up);
            else if (Input.GetKeyUp(KeyCode.D))
                InvokeFlipperAction(Side.Right, FlipperDirection.Down);

            else if (Input.GetKeyUp(KeyCode.T))
                OnStartPressed?.Invoke();
        }
    }

    internal void InvokeFlipperAction(Side side, FlipperDirection direction)
    {
        OnFlipperAction?.Invoke(side, direction);
    }
}
