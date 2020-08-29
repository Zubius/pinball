using System;
using UnityEngine;

internal class BaseScoreObject : MonoBehaviour
{
    [SerializeField] protected int scoreAmount;
    [SerializeField] protected float additionalForce = 1;
    [SerializeField] protected ScoreObjectType type = ScoreObjectType.Simple;

    protected virtual void OnCollisionEnter(Collision ball)
    {
        var ballRigidbody = ball.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            GameController.Instance.ScoreObject(type, scoreAmount);

            Debug.DrawRay(ball.contacts[0].point, ball.contacts[0].normal * additionalForce * -1);
            ballRigidbody.AddForce(additionalForce * ball.contacts[0].normal * -1);
        }
    }
}

internal enum ScoreObjectType
{
    Simple,
    Grouped,
}
