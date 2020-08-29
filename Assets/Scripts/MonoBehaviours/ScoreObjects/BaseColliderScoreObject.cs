using System;
using UnityEngine;

internal abstract class BaseColliderScoreObject : BaseScoreObject
{
    private void OnCollisionEnter(Collision ball)
    {
        var ballRigidbody = ball.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            CollisionWithBallHandler(ball, ballRigidbody);
        }
    }

    protected virtual void CollisionWithBallHandler(Collision ballCollision, Rigidbody ball)
    {
        GameController.Instance.ScoreObject(id, scoreAmount, _task?.Id);

        ball.AddForce(additionalForce * ballCollision.contacts[0].normal * -1);
    }
}
