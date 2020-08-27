using System;
using UnityEngine;

internal class ScoreObject : MonoBehaviour
{
    [SerializeField] private int ScoreAmount;

    private void OnCollisionEnter(Collision other)
    {
        GameController.Instance.ScoreController.AddScores(ScoreAmount);
    }
}
