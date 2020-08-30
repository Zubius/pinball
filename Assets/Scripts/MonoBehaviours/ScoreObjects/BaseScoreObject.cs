using System;
using Unity.Collections;
using UnityEngine;

internal abstract class BaseScoreObject : MonoBehaviour
{
    [SerializeField] protected int scoreAmount;
    [SerializeField] protected float additionalForce = 1;
    [SerializeField] protected int id;

    internal ScoreAbstractTask Task { get; private set; }
    internal int Id => id;

    internal void SetTask(ScoreAbstractTask task)
    {
        Task = task;
    }

    internal void RemoveTask()
    {
        if (Task?.CheckCompleted() ?? false)
            Task = null;
    }

    internal void SetId(int id)
    {
        this.id = id;
    }

    internal abstract ScoreObjectType Type { get; }
}

internal enum ScoreObjectType
{
    None = 0,
    Simple,
    Grouped,
    Chained,
}
