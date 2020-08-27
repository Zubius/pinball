internal class GameProcessState : BaseState
{
    internal override GameState State => GameState.GameProcess;

    private void MoveFlipper(Side side, float _)
    {
        switch (side)
        {
            case Side.Left:
                GameController.Instance.MoveLeftFlipper();
                break;
            case Side.Right:
                GameController.Instance.MoveRightFlipper();
                break;
        }
    }

    private void UpdateScores()
    {
        GameController.Instance.UpdateScores();
    }
}
