using System;

internal class ScoreObjectController
{
    internal event Action<ScoreObjectType, int> OnScored;

    internal void ScoreObject(ScoreObjectType type, int scores)
    {
        OnScored?.Invoke(type, scores);
    }
}
