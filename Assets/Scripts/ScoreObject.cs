using System;
using UnityEngine;

internal class ScoreObject : MonoBehaviour
{
    [SerializeField] private int scoreAmount;

    private void OnCollisionEnter(Collision other)
    {
        GameController.Instance.ScoreController.AddScores(scoreAmount);
    }
}
