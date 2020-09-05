using System;

internal sealed class LoseBallState : BaseGameState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter(IPinballContext context)
    {
        context.HandleDroppedBall();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, IPinballContext context, out GameState nextState)
    {
        nextState = GameState.None;
        switch (gameEvent)
        {
            case GameEvent.HandleBallDropped:
                if (DataController.BallsController.BallsCount > 0)
                {
                    context.PrepareLaunchBall();
                    nextState = GameState.LaunchBall;
                }
                else
                {
                    nextState = GameState.EndGame;
                }
                return true;
        }

        return false;
    }
}
