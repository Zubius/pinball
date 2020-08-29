using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class UIController : MonoBehaviour
{
    [SerializeField] private Text topScoresText;
    [SerializeField] private Text currentScores;
    [SerializeField] private Text finalScores;
    [SerializeField] private Text topScoresEndGameText;
    [SerializeField] private Text ballsLeft;
    [SerializeField] private Text startGameInfo;
    [SerializeField] private Text endGameInfo;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Toggle aiToggle;
    [SerializeField] private EventTrigger leftTrigger;
    [SerializeField] private EventTrigger rightTrigger;

    internal bool AIToggleIsON => aiToggle.isOn;

    private readonly string _startGameKeyboardInfoGame = "Hold Space for launch ball\nUse A and D for left and right flippers";
    private readonly string _startGameKeyboardInfoStart = "Press T for start game";
    private readonly string _startGameTouchInfo = "Hold touch for launch ball\nPress left or right part of screen for flippers";
    private readonly string _endGameInfo = "Press R to restart";

    private void Start()
    {
        aiToggle.onValueChanged.AddListener(OnAiToggled);
    }

    private void SetTopScores(Text textObject, int topScores)
    {
        textObject.SetText($"Top Scores: {topScores.ToString()}");
    }

    internal void SetStartButtonAction(Action startAction)
    {
        startButton.onClick.AddListener(new UnityAction(startAction));
    }

    internal void SetRestartButtonAction(Action startAction)
    {
        restartButton.onClick.AddListener(new UnityAction(startAction));
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
            case FlipperDirection.Down:
                entry.eventID = EventTriggerType.PointerUp;
                break;
            case FlipperDirection.Up:
                entry.eventID = EventTriggerType.PointerDown;
                break;
        }

        entry.callback.AddListener((e) => action());
        trigger.triggers.Add(entry);
    }

    internal void SetCurrentScores(int scores)
    {
        currentScores.SetText($"Scores:\n{scores.ToString()}");
    }

    internal void SetBallsLeft(int count)
    {
        ballsLeft.SetText($"Balls left:\n{count.ToString()}");
    }

    internal void ShowStartScreenWithTopScores(int topScores, bool showButton)
    {
        SetTopScores(topScoresText, topScores);
        ShowStartScreen();

        startGameInfo.SetText(showButton ? _startGameTouchInfo : $"{_startGameKeyboardInfoStart}\n{_startGameKeyboardInfoGame}");

        startButton.gameObject.SetActive(showButton);
    }

    internal void ShowEndGameScreen(int topScores, int currentScores, bool showButton)
    {
        SetTopScores(topScoresEndGameText,topScores);
        finalScores.SetText($"Final Scores: {currentScores.ToString()}");
        restartButton.gameObject.SetActive(showButton);
        endGameInfo.enabled = !showButton;
        ShowEndScreen();
    }

    internal void HideStartScreen()
    {
        ShowScreen(startScreen, false);
    }

    private void ShowStartScreen()
    {
        ShowScreen(startScreen, true);
        ShowScreen(endScreen, false);
    }

    private void OnAiToggled(bool toggled)
    {
        if (startButton.gameObject.activeSelf)
        {
            startGameInfo.enabled = !toggled;
            return;
        }

        startGameInfo.SetText(toggled ? _startGameKeyboardInfoStart : $"{_startGameKeyboardInfoStart}\n{_startGameKeyboardInfoGame}");
    }

    private void ShowEndScreen()
    {
        ShowScreen(endScreen, true);
    }

    private void ShowScreen(GameObject screen, bool enabled)
    {
        if (screen.activeSelf != enabled)
            screen.SetActive(enabled);
    }


}
