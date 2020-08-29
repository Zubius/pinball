using System.Collections.Generic;

internal abstract class ScoreAbstractTask
{
    internal readonly int Id;
    internal readonly int[] ObjectIds;
    internal readonly ScoreObjectType ForType;
    internal readonly int Reward;

    private HashSet<int> _objects;

    internal ScoreAbstractTask(int id, int[] objects, int reward, ScoreObjectType forType)
    {
        Id = id;
        ObjectIds = objects;
        ForType = forType;
        Reward = reward;

        _objects = new HashSet<int>(ObjectIds);
    }

    internal bool ContainsId(int id)
    {
        return _objects.Contains(id);
    }

    internal bool CheckCompleted()
    {
        return false;
    }
}
