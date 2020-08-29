internal class GameProcessState : BaseState
{
    internal override GameState State => GameState.GameProcess;


    internal override void OnStateEnter()
    {
        DataController.InputSource.OnFlipperAction += MoveFlipper;
        DataController.GameScoreController.OnScoreChanged += UpdateGameScores;
        DataController.ScoreObjectController.OnScored += OnScored;
    }

    private void OnScored(ScoreObjectType type, int scores)
    {
        DataController.GameScoreController.AddScores(scores);
    }

    internal override void OnStateExit()
    {
        Controller.MoveLeftFlipper(FlipperDirection.Down);
        Controller.MoveRightFlipper(FlipperDirection.Down);
        DataController.InputSource.OnFlipperAction -= MoveFlipper;
        DataController.GameScoreController.OnScoreChanged -= UpdateGameScores;
        DataController.ScoreObjectController.OnScored -= OnScored;
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
        Controller.UpdateScores(DataController.GameScoreController.CurrentScores);
    }

    public GameProcessState(GameController controller) : base(controller) { }
}
