using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

internal class AIInputHandler : MonoBehaviour, IInputSource
{
    [SerializeField] private AITrigger leftTrigger;
    [SerializeField] private AITrigger rightTrigger;

    private readonly WaitForSeconds _startWaiter = new WaitForSeconds(2);
    private readonly WaitForSeconds _flipperWaiter = new WaitForSeconds(0.2f);

    private Coroutine _startCoroutine;

    private void Start()
    {
        Restart();
    }

    internal void Restart()
    {
        if (_startCoroutine != null)
            StopCoroutine(_startCoroutine);

        _startCoroutine = StartCoroutine(StartAI());
    }

    private IEnumerator StartAI()
    {
        leftTrigger.OnTriggered -= LeftTriggerOnOnTriggered;
        rightTrigger.OnTriggered -= RightTriggerOnOnTriggered;

        leftTrigger.OnTriggered += LeftTriggerOnOnTriggered;
        rightTrigger.OnTriggered += RightTriggerOnOnTriggered;

        return StartGame();
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
        yield return _startWaiter;

        OnLaunchReleased?.Invoke(Random.Range(0.8f, 1.5f));
    }

    private IEnumerator MoveFlipper(Side side)
    {
        OnFlipperAction?.Invoke(side, FlipperDirection.Up);

        yield return _flipperWaiter;

        OnFlipperAction?.Invoke(side, FlipperDirection.Down);
    }
}
