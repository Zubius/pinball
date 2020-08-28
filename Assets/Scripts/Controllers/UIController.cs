using UnityEngine;
using UnityEngine.UI;

internal class UIController : MonoBehaviour
{
    [SerializeField] private Text topScoresText;
    [SerializeField] private Text currentScores;
    [SerializeField] private Text ballsLeft;
    [SerializeField] private GameObject startScreen;

    internal void SetTopScores(int topScores)
    {
        topScoresText.text = $"Top Scores: {topScores.ToString()}";
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
