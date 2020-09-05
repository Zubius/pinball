internal interface IStateMachineContext
{

}

internal interface IPinballContext : IStateMachineContext
{
    void SetNewGame();

    void SetNewRound();

    void PrepareLaunchBall();

    void LaunchBall(BallType type, float holdDuration);

    void MoveFlipper(Side side, FlipperDirection direction);

    void AddScore(int scores);

    void CompleteTask(int taskId);

    void HandleDroppedBall();

    void SetEndGame();

    void Restart();
}
