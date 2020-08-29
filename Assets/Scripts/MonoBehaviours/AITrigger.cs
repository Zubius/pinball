using System;
using UnityEngine;

internal class AITrigger : MonoBehaviour
{
    internal event Action OnTriggered;

    private void OnTriggerEnter(Collider ball)
    {
        if (ball.GetComponent<Ball>() != null)
        {
            OnTriggered?.Invoke();
        }
    }
}
