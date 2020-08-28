using System;
using JetBrains.Annotations;
using UnityEngine;

internal class LaunchBallController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;

    internal event Action<Ball> OnBallLaunched;

    internal void LaunchBall(float force)
    {
        var ball = Instantiate(Resources.Load<Ball>("Ball"));

        ball.transform.position = launchPoint.position;

        ball.Launch(force);
        OnBallLaunched?.Invoke(ball);
    }
}
