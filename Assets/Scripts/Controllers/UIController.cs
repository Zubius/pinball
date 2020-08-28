using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class UIController : MonoBehaviour
{
    [SerializeField] private Text topScoresText;
    [SerializeField] private Text currentScores;
    [SerializeField] private Text ballsLeft;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private Button startButton;
    [SerializeField] private EventTrigger leftTrigger;
    [SerializeField] private EventTrigger rightTrigger;

    internal void SetTopScores(int topScores)
    {
        topScoresText.text = $"Top Scores: {topScores.ToString()}";
    }

    internal void SetStartButtonAction(Action startAction)
    {
        startButton.onClick.AddListener(new UnityAction(startAction));
    }

    internal void SetEventTrigger(Side side, FlipperDirection direction, Action action)
    {
        switch (side)
        {
            case Side.Left:
                SetTriggerAction(leftTrigger, direction, action);
                break;
            case Side.Right:
                SetTriggerAction(rightTrigger, direction, action);
                break;
        }
    }

    private void SetTriggerAction(EventTrigger trigger, FlipperDirection direction, Action action)
    {
        var entry = new EventTrigger.Entry();

        switch (direction)
        {
            case FlipperDirection.Up:
                entry.eventID = EventTriggerType.PointerUp;
                break;
            case FlipperDirection.Down:
                entry.eventID = EventTriggerType.PointerDown;
                break;
        }

        entry.callback.AddListener((e) => action());
        trigger.triggers.Add(entry);
    }

    internal void SetCurrentScores(int scores)
    {
        currentScores.text = $"Scores:\n{scores.ToString()}";
    }

    internal void SetBallsLeft(int count)
    {
        ballsLeft.text = $"Balls left:\n{count}";
    }

    internal void ShowStartScreenWithTopScores(int topScores)
    {
        SetTopScores(topScores);
        ShowStartScreen(true);
    }

    internal void HideStartScreen()
    {
        ShowStartScreen(false);
    }

    private void ShowStartScreen(bool enabled)
    {
        if (startScreen.activeSelf != enabled)
            startScreen.SetActive(enabled);
    }


}
