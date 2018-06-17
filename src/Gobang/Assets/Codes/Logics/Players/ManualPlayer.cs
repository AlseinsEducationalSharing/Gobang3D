using System.Threading.Tasks;

class ManualPlayer : Player
{
    private Game _game;

    public ManualPlayer(Game game)
    {
        _game = game;
        _game.UserIO.Initialize();
    }

    public override Task<Point> GetNext()
    {
        return _game.UserIO.GetClickPointOnChessboard();
    }

    protected override void OnChanged()
    {
        _game.UserIO.ChessboardData = GameData;
    }
}
