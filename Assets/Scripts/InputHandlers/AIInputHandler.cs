using System;
using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

internal class AIInputHandler : MonoBehaviour, IInputSource
{
    [SerializeField] private AITrigger leftTrigger;
    [SerializeField] private AITrigger rightTrigger;

    private readonly WaitForSeconds _startWaiter = new WaitForSeconds(2);
    private readonly WaitForSeconds _flipperWaiter = new WaitForSeconds(0.2f);

    private IEnumerator Awake()
    {
        leftTrigger.OnTriggered += LeftTriggerOnOnTriggered;
        rightTrigger.OnTriggered += RightTriggerOnOnTriggered;

        yield return StartGame();
    }

    private void RightTriggerOnOnTriggered()
    {
        StartCoroutine(MoveFlipper(Side.Right));
    }

    private void LeftTriggerOnOnTriggered()
    {
        StartCoroutine(MoveFlipper(Side.Left));
    }

    public event Action<float> OnLaunchReleased;

    public event Action<Side, FlipperDirection> OnFlipperAction;

    public event Action OnStartPressed;
    public event Action OnRestartPressed;

    private IEnumerator StartGame()
    {
        OnStartPressed?.Invoke();

        yield return _startWaiter;

        OnLaunchReleased?.Invoke(1f);
    }

    private IEnumerator MoveFlipper(Side side)
    {
        OnFlipperAction?.Invoke(side, FlipperDirection.Up);

        yield return _flipperWaiter;

        OnFlipperAction?.Invoke(side, FlipperDirection.Down);
    }
}
