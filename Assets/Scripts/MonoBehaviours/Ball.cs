using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
internal class Ball : MonoBehaviour
{
    private Rigidbody _cachedRigidbody;

    [SerializeField] internal BallType type = BallType.Simple;

    private void Awake()
    {
        _cachedRigidbody = GetComponent<Rigidbody>();
    }

    internal void Launch(float force)
    {
        _cachedRigidbody.AddForce(Vector3.forward * force);
    }
}
