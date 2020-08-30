using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class GroupScoreTask : ScoreAbstractTask
{
    private Dictionary<int, bool> _groupHits;

    public GroupScoreTask(int id, int[] objects, int reward, ScoreObjectType forType) : base(id, objects, reward, forType)
    {
        _groupHits = objects.ToDictionary(o => o, o => false);
    }

    internal override bool AddProgress(int objectId)
    {
        if (_groupHits.ContainsKey(objectId))
        {
            _groupHits[objectId] = true;
            return true;
        }

        return false;
    }

    internal override void ResetCondition()
    {

    }

    internal override bool CheckCompleted()
    {
        if (_isComplete)
            return true;

        bool completed = true;
        foreach (var hit in _groupHits.Values)
        {
            completed &= hit;
        }

        _isComplete = completed;

        return _isComplete;
    }
}
