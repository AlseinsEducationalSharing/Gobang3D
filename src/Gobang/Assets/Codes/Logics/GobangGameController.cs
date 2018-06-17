class GobangGameController : GameController
{
    private GobangGame Game;
    private Player BlackPlayer, WhitePlayer;

    protected override void InitializeGame()
    {
        Game = new GobangGame();
        BlackPlayer = new ManualPlayer();
        WhitePlayer = new ManualPlayer();
    }

    protected override bool HandleGame()
    {
        var player = Game.Current.NextPlayer == Faction.Black ? BlackPlayer : WhitePlayer;
        var pos = player.GetNext();
        if (Game.PositionAvailable(pos))
        {
            Game.Next(pos);
            player.GameData = Game.Current;
            if (Game.CheckForWin(pos.X, pos.Y))
            {
                MessageBox.Show(string.Format("{0}方胜利！", Game.Current.CurrentPlayer == Faction.Black ? "黑" : "白"));
                return false;
            }
        }
        return true;
    }

    protected override void FinalizeGame()
    {
        UserIO.ChessboardData = new GameSnapshot();
    }
}
