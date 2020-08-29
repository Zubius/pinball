internal class StartGameState : BaseState
{
    internal override GameState State => GameState.StartGame;

    internal override void OnStateEnter()
    {
        Controller.ShowNewGameScreen(DataController.GameScoreController.TopScores);
        DataController.InputSource.OnStartPressed += Controller.StartGame;
    }

    internal override void OnStateExit()
    {
        DataController.InputSource.OnStartPressed -= Controller.StartGame;
    }

    public StartGameState(GameController controller) : base(controller) { }
}
