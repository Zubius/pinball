using UnityEngine;

internal class GroupedScoreObject : SimpleScoreObject
{
    [SerializeField] private GroupId groupId;

    internal GroupId GroupId => groupId;

    protected override ScoreObjectType Type => ScoreObjectType.Grouped;
}

internal enum GroupId
{
    First = 0,
}
