
using UnityEngine;

internal class ScoreController
{
    internal int CurrentScores { get; private set; }
    internal int TopScores { get; private set; }

    private readonly string _topScoresKey = "top_scores";
    private bool _topScoresUpdated;

    internal ScoreController()
    {
        TopScores = PlayerPrefs.GetInt(_topScoresKey, 0);
        CurrentScores = 0;
    }

    internal void Reset()
    {
        CurrentScores = 0;
    }

    internal void SaveScores()
    {
        if (_topScoresUpdated)
        {
            PlayerPrefs.SetInt(_topScoresKey, TopScores);
            PlayerPrefs.Save();
        }
    }

    internal void AddScores(int inc)
    {
        CurrentScores += inc;

        if (CurrentScores > TopScores)
        {
            TopScores = CurrentScores;
            _topScoresUpdated = true;
        }
    }
}
