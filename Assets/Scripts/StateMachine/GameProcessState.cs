internal class GameProcessState : BaseState
{
    internal override GameState State => GameState.GameProcess;


    internal override void OnStateEnter()
    {
        Controller.InputSource.OnFlipperAction += MoveFlipper;
        Controller.GameScoreController.OnScoreChanged += UpdateGameScores;
        Controller.ScoreObjectController.OnScored += OnScored;
    }

    private void OnScored(ScoreObjectType type, int scores)
    {
        Controller.GameScoreController.AddScores(scores);
    }

    internal override void OnStateExit()
    {
        Controller.MoveLeftFlipper(FlipperDirection.Down);
        Controller.MoveRightFlipper(FlipperDirection.Down);
        Controller.InputSource.OnFlipperAction -= MoveFlipper;
        Controller.GameScoreController.OnScoreChanged -= UpdateGameScores;
    }

    private void MoveFlipper(Side side, FlipperDirection direction)
    {
        switch (side)
        {
            case Side.Left:
                Controller.MoveLeftFlipper(direction);
                break;
            case Side.Right:
                Controller.MoveRightFlipper(direction);
                break;
        }
    }

    private void UpdateGameScores()
    {
        Controller.UpdateScores();
    }

    public GameProcessState(GameController controller) : base(controller) { }
}
