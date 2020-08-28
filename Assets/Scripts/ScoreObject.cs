using System;
using UnityEngine;

internal class ScoreObject : MonoBehaviour
{
    [SerializeField] private int scoreAmount;
    [SerializeField] private float additionalForce;

    private void OnCollisionEnter(Collision other)
    {
        GameController.Instance.ScoreController.AddScores(scoreAmount);

        var ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal);
            ballRigidbody.AddForce(additionalForce * other.contacts[0].normal);
        }
    }
}
