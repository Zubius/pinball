using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
internal class Ball : MonoBehaviour
{
    [SerializeField] private Transform cachedTransform;
    [SerializeField] private Rigidbody cachedRigidbody;

    private void Awake()
    {
        if (cachedTransform == null)
            cachedTransform = transform;

        if (cachedRigidbody == null)
            cachedRigidbody = GetComponent<Rigidbody>();
    }

    internal void Launch(float force)
    {
        cachedRigidbody.AddForce(Vector3.forward * force);
    }
}
