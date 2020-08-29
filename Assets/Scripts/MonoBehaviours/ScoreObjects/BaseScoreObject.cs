using System;
using Unity.Collections;
using UnityEngine;

internal abstract class BaseScoreObject : MonoBehaviour
{
    [SerializeField] protected int scoreAmount;
    [SerializeField] protected float additionalForce = 1;
    [SerializeField] protected int id;

    protected ScoreAbstractTask _task;

    internal void SetTask(ScoreAbstractTask task)
    {
        _task = task;
    }

    internal void SetId(int id)
    {
        this.id = id;
    }

    protected abstract ScoreObjectType Type { get; }
}

internal enum ScoreObjectType
{
    None = 0,
    Simple,
    Grouped,
    Chained,
}
