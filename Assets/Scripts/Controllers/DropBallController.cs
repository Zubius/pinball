using System;
using UnityEngine;

internal class DropBallController : MonoBehaviour
{
    internal event Action OnBallDropped;

    private void OnTriggerExit(Collider other)
    {
        OnBallDropped?.Invoke();
    }
}
