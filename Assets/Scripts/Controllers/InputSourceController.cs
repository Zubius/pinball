using System;
using System.Collections.Generic;

internal class InputSourceController
{
    internal event Action<float> OnLaunchReleased;
    internal event Action<Side, FlipperDirection> OnFlipperAction;
    internal event Action OnStartPressed;
    internal event Action OnRestartPressed;

    private List<IInputSource> _inputSources = new List<IInputSource>();

    internal void AddSource(IInputSource source)
    {
        SubscribeToInput(source);
        _inputSources.Add(source);
    }

    internal bool RemoveSource(IInputSource source)
    {
        UnsbscribeFromInput(source);
        return _inputSources.Remove(source);
    }

    internal void ReplaceSource(IInputSource oldSource, IInputSource newSource)
    {
        RemoveSource(oldSource);
        AddSource(newSource);
    }

    internal void SetSingleSource(IInputSource source)
    {
        foreach (var inputSource in _inputSources)
        {
            UnsbscribeFromInput(inputSource);
        }

        _inputSources.Clear();

        AddSource(source);
    }

    private void SubscribeToInput(IInputSource source)
    {
        source.OnStartPressed += OnStartPressedAction;
        source.OnRestartPressed += OnRestartPressedAction;
        source.OnLaunchReleased += OnLaunchReleasedAction;
        source.OnFlipperAction += OnFlipperActionAction;
    }

    private void UnsbscribeFromInput(IInputSource source)
    {
        source.OnStartPressed -= OnStartPressedAction;
        source.OnRestartPressed -= OnRestartPressedAction;
        source.OnLaunchReleased -= OnLaunchReleasedAction;
        source.OnFlipperAction -= OnFlipperActionAction;
    }

    private void OnFlipperActionAction(Side arg1, FlipperDirection arg2)
    {
        OnFlipperAction?.Invoke(arg1, arg2);
    }

    private void OnLaunchReleasedAction(float obj)
    {
        OnLaunchReleased?.Invoke(obj);
    }

    private void OnRestartPressedAction()
    {
        OnRestartPressed?.Invoke();
    }

    private void OnStartPressedAction()
    {
        OnStartPressed?.Invoke();
    }
}
