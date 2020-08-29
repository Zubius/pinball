using System;
using UnityEngine;

internal class ScoreObjectController : MonoBehaviour
{
    [SerializeField] private BaseScoreObject[] scoreObjects;

    internal void ScoreObject(ScoreObjectType type, int scores)
    {
        OnScored?.Invoke(type, scores);
    }

    internal event Action<ScoreObjectType, int> OnScored;
}
