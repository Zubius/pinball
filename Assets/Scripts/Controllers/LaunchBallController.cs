using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

internal class LaunchBallController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Ball[] balls;

    internal event Action<Ball> OnBallLaunched;

    internal void LaunchBall(float force)
    {
        var ball = Instantiate(balls[Random.Range(0, balls.Length - 1)]);

        ball.transform.position = launchPoint.position;

        ball.Launch(force);
        OnBallLaunched?.Invoke(ball);
    }
}
