using UnityEngine;

internal class GroupedScoreObject : SimpleScoreObject
{
    [SerializeField] private GroupId groupId;

    internal GroupId GroupId => groupId;

    internal override ScoreObjectType Type => ScoreObjectType.Grouped;

    protected override void CollisionWithBallHandler(Collision ballCollision, Rigidbody ball)
    {
        base.CollisionWithBallHandler(ballCollision, ball);

        Task?.AddProgress(id);
    }
}

internal enum GroupId
{
    First = 0,
}
