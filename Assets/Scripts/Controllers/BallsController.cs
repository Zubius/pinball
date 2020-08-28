internal class BallsController
{
    internal int BallsCount { get; private set; }


    internal BallsController(int initCount)
    {
        BallsCount = initCount;
    }

    internal void DecrementBallsCount()
    {
        BallsCount--;
    }
}

internal enum BallType
{
    Simple = 0,
    Rapid,
    DoubleScore
}
