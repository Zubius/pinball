using System;

internal class LoseBallState : BaseGameState
{
    internal override GameState State => GameState.LoseBall;

    internal override void OnStateEnter()
    {
        GameController.Instance.SetBallDropped();
    }

    internal override bool ProcessEvent(GameEvent gameEvent, IInputContainer _, out GameState nextState)
    {
        nextState = GameState.None;
        switch (gameEvent)
        {
            case GameEvent.HandleBallDropped:
                if (DataController.BallsController.BallsCount > 0)
                {
                    GameController.Instance.PlatNextBall();
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
