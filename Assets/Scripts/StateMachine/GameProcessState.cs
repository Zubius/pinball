internal class GameProcessState : BaseState
{
    internal override GameState State => GameState.GameProcess;


    internal override void OnStateEnter()
    {
        Controller.InputSource.OnFlipperAction += MoveFlipper;
        Controller.ScoreController.OnScoreChanged += UpdateScores;
    }

    internal override void OnStateExit()
    {
        Controller.MoveLeftFlipper(FlipperDirection.Down);
        Controller.MoveRightFlipper(FlipperDirection.Down);
        Controller.InputSource.OnFlipperAction -= MoveFlipper;
        Controller.ScoreController.OnScoreChanged -= UpdateScores;
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

    private void UpdateScores()
    {
        Controller.UpdateScores();
    }

    public GameProcessState(GameController controller) : base(controller) { }
}
