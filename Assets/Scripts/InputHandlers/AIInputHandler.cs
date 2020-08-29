using System;

internal class AIInputHandler : IInputSource
{
    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;
    public event Action OnRestartPressed;
}
