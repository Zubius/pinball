using System;
using System.Collections.Generic;
using System.Linq;
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

    internal void AssignTaskToObjects(ScoreAbstractTask task, int[] objectIds)
    {
        foreach (var scoreObject in scoreObjects)
        {
            if (Array.IndexOf(objectIds, scoreObject.Id) > -1)
            {
                scoreObject.SetTask(task);
            }
        }
    }

    internal void RemoveTaskFromObjects(int taskId)
    {
        foreach (var scoreObject in scoreObjects)
        {
            if (scoreObject.Task?.Id == taskId)
            {
                scoreObject.RemoveTask();
            }
        }
    }

    internal int[] GetObjectIdsByType(ScoreObjectType type)
    {
        var objs = new List<int>();
        for (int i = 0; i < scoreObjects.Length; i++)
        {
            if (scoreObjects[i].Type == type)
                objs.Add(scoreObjects[i].Id);
        }

        return objs.ToArray();
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

    public override string ToString()
    {
        return $"Scored Id: {Id.ToString()}, Scores To Add: {Scores.ToString()}, Task Id: {(TaskId.HasValue ? TaskId.Value.ToString() : string.Empty)}";
    }
}
