using System.Threading.Tasks;

internal class GobangGameWorkflow : GameWorkflow
{
    private GobangGameData GobangGame;
    private Game _game;
    private Player BlackPlayer, WhitePlayer;

    public GobangGameWorkflow(Game game) => _game = game;

    protected override void InitializeGame()
    {
        GobangGame = new GobangGameData();
        BlackPlayer = new ManualPlayer(_game);
        WhitePlayer = new ManualPlayer(_game);
    }

    protected override async Task<bool> HandleGame()
    {
        var player = GobangGame.Current.NextPlayer == Faction.Black ? BlackPlayer : WhitePlayer;
        var pos = await player.GetNext();
        if (GobangGame.PositionAvailable(pos))
        {
            GobangGame.Next(pos);
            player.GameData = GobangGame.Current;
            if (GobangGame.CheckForWin(pos.X, pos.Y))
            {
                await _game.ShowMessage(string.Format("{0}方胜利！", GobangGame.Current.CurrentPlayer == Faction.Black ? "黑" : "白"));
                return false;
            }
        }
        return true;
    }

    protected override void FinalizeGame() => _game.UserIO.ChessboardData = new GameSnapshot();
}
