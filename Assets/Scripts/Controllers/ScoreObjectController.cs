using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

internal class ScoreObjectController : MonoBehaviour
{
    [SerializeField] private BaseScoreObject[] scoreObjects;

    private void Awake()
    {
        for (int i = 0; i < scoreObjects.Length; i++)
        {
            scoreObjects[i].SetId(i);
        }
    }

    internal void ScoreObject(int id, int scores, int? taskId)
    {
        OnScored?.Invoke(new ScoreObjectArgs(id, scores, taskId));
    }

    internal event Action<ScoreObjectArgs> OnScored;
}

internal struct ScoreObjectArgs
{
    internal readonly int Id;
    internal readonly int Scores;
    internal readonly int? TaskId;

    internal ScoreObjectArgs(int id, int scores, int? taskId)
    {
        Id = id;
        Scores = scores;
        TaskId = taskId;
    }
}
