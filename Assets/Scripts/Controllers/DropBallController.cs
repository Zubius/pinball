using System;
using UnityEngine;

internal class DropBallController : MonoBehaviour
{
    internal event Action<GameObject> OnBallDropped;

    private void OnTriggerExit(Collider other)
    {
        OnBallDropped?.Invoke(other.gameObject);
    }
}
