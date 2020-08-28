using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

internal class LaunchBallController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Ball[] balls;

    private Dictionary<BallType, Ball> _ballsByType;

    private void Awake()
    {
        _ballsByType = new Dictionary<BallType, Ball>(balls.Length, new EnumComparer<BallType>());

        foreach (var ball in balls)
        {
            _ballsByType[ball.type] = ball;
        }
    }

    internal event Action<Ball> OnBallLaunched;

    internal void LaunchBall(BallType type, float force)
    {
        var ball = Instantiate(_ballsByType[type]);

        ball.transform.position = launchPoint.position;

        ball.Launch(force);
        OnBallLaunched?.Invoke(ball);
    }
}
