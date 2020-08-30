internal class GameProcessState : BaseAbstractState
{
    internal override GameState State => GameState.GameProcess;


    internal override void OnStateEnter()
    {
        DataController.InputSource.OnFlipperAction += MoveFlipper;
        DataController.GameScoreController.OnScoreChanged += UpdateGameScores;
        DataController.ScoreObjectController.OnScored += OnScored;
    }

    private void OnScored(ScoreObjectArgs scoreObjectArgs)
    {
        DataController.GameScoreController.AddScores(scoreObjectArgs.Scores);

        if (scoreObjectArgs.TaskId.HasValue && DataController.ScoreTaskController.CheckTaskCompleteById(scoreObjectArgs.TaskId.Value, out int reward))
        {
            DataController.ScoreObjectController.RemoveTaskFromObjects(scoreObjectArgs.TaskId.Value);
            DataController.GameScoreController.AddScores(reward);
        }
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
