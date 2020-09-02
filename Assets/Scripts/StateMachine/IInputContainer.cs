internal interface IInputContainer
{

}

internal class LaunchInput : IInputContainer
{
    internal readonly float HoldDuration;

    internal LaunchInput(float duration)
    {
        HoldDuration = duration;
    }

    public override string ToString()
    {
        return $"Hold Duration: {HoldDuration.ToString()}";
    }
}

internal class FlipperInput : IInputContainer
{
    internal readonly Side FlipperSide;
    internal readonly FlipperDirection FlipperDirection;

    internal FlipperInput(Side side, FlipperDirection direction)
    {
        FlipperSide = side;
        FlipperDirection = direction;
    }

    public override string ToString()
    {
        return $"Side: {FlipperSide.ToString()}, Direction: {FlipperDirection.ToString()}";
    }
}

internal class ScoreInput : IInputContainer
{
    internal readonly ScoreObjectArgs ScoreArgs;

    internal ScoreInput(ScoreObjectArgs scoreArgs)
    {
        ScoreArgs = scoreArgs;
    }

    public override string ToString()
    {
        return ScoreArgs.ToString();
    }
}
