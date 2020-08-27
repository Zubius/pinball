using UnityEngine;

internal class LaunchBallController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;

    private Ball _ball;

    internal void LaunchBall(float force)
    {
        if (_ball == null)
            _ball = Instantiate(Resources.Load<Ball>("Ball"));

        _ball.transform.position = launchPoint.position;
        //TODO debug
        // _ball.Launch(force);
    }
}
