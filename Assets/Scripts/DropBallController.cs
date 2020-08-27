using System;
using UnityEngine;

internal class DropBallController : MonoBehaviour
{
    internal event Action OnBallDropped;

    private void OnTriggerEnter(Collider other)
    {
        OnBallDropped?.Invoke();
    }
}
