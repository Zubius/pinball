using System;
using UnityEngine;

internal class BaseScoreObject : MonoBehaviour
{
    [SerializeField] private int scoreAmount;
    [SerializeField] private float additionalForce;
    [SerializeField] private ScoreObjectType type = ScoreObjectType.Simple;

    protected virtual void OnCollisionEnter(Collision ball)
    {
        var ballRigidbody = ball.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            GameController.Instance.ScoreObjectController.ScoreObject(type, scoreAmount);

            Debug.DrawRay(ball.contacts[0].point, ball.contacts[0].normal);
            ballRigidbody.AddForce(additionalForce * ball.contacts[0].normal);
        }
    }
}

internal enum ScoreObjectType
{
    Simple,
    Grouped,
}
