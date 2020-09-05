using System;
using CoreHapticsUnity;

internal sealed class GameProcessState : BaseGameState
{
    internal override GameState State => GameState.GameProcess;

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer data, IPinballContext context, out GameState nextState)
    {
        nextState = GameState.None;

        switch (gameEvent)
        {
            case GameEvent.MoveFlipper:
                if (data is FlipperInput flipper)
                    context.MoveFlipper(flipper.FlipperSide, flipper.FlipperDirection);
                break;
            case GameEvent.DropBall:
                nextState = GameState.LoseBall;
                return true;
            case GameEvent.AddScores:
                if (data is ScoreInput scoreData)
                    OnScored(scoreData.ScoreArgs);
                break;
        }
        return false;

        void OnScored(ScoreObjectArgs scoreObjectArgs)
        {
            int scores = scoreObjectArgs.Scores;
            if (scoreObjectArgs.TaskId.HasValue && DataController.ScoreTaskController.CheckTaskCompleteById(scoreObjectArgs.TaskId.Value, out int reward))
            {
                context.CompleteTask(scoreObjectArgs.TaskId.Value);
                scores += reward;
            }
            context.AddScore(scores);
        }
    }


    internal override void OnStateExit(IPinballContext context)
    {
        context.MoveFlipper(Side.Left, FlipperDirection.Down);
        context.MoveFlipper(Side.Right, FlipperDirection.Down);
    }
}
