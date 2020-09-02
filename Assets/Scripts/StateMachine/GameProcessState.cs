using System;
using CoreHapticsUnity;

internal class GameProcessState : BaseGameState
{
    internal override GameState State => GameState.GameProcess;

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, out GameState nextState)
    {
        nextState = GameState.None;

        switch (gameEvent)
        {
            case GameEvent.MoveFlipper:
                if (data is FlipperInput flipper)
                    MoveFlipper(flipper.FlipperSide, flipper.FlipperDirection);
                break;
            case GameEvent.DropBall:
                nextState = GameState.LoseBall;
                return true;
            case GameEvent.AddScores:
                if (data is ScoreInput scoreData)
                    OnScored(scoreData.ScoreArgs);
                break;
            case GameEvent.UpdateScores:
                UpdateGameScores();
                break;
        }
        return false;
    }

    private void OnScored(ScoreObjectArgs scoreObjectArgs)
    {
        DataController.GameScoreController.AddScores(scoreObjectArgs.Scores);

        CoreHapticsUnityProxy.PlayTransient(0.5f, 1f);

        if (scoreObjectArgs.TaskId.HasValue && DataController.ScoreTaskController.CheckTaskCompleteById(scoreObjectArgs.TaskId.Value, out int reward))
        {
            DataController.ScoreObjectController.RemoveTaskFromObjects(scoreObjectArgs.TaskId.Value);
            DataController.GameScoreController.AddScores(reward);
        }
    }

    internal override void OnStateExit()
    {
        GameController.Instance.MoveLeftFlipper(FlipperDirection.Down);
        GameController.Instance.MoveRightFlipper(FlipperDirection.Down);
    }

    private void MoveFlipper(Side side, FlipperDirection direction)
    {
        if (direction == FlipperDirection.Up)
            CoreHapticsUnityProxy.PlayTransient(0.5f, 1f);

        switch (side)
        {
            case Side.Left:
                GameController.Instance.MoveLeftFlipper(direction);
                break;
            case Side.Right:
                GameController.Instance.MoveRightFlipper(direction);
                break;
        }
    }

    private void UpdateGameScores()
    {
        GameController.Instance.UpdateScores(DataController.GameScoreController.CurrentScores);
    }
}
