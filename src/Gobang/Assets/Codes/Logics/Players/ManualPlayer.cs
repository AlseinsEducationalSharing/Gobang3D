using System.Threading.Tasks;

internal class ManualPlayer : Player
{
    private Game _game;

    public ManualPlayer(Game game)
    {
        _game = game;
        _game.UserIO.Initialize();
    }

    public override Task<Point> GetNext() => _game.UserIO.GetClickPoint();

    protected override void OnChanged() => _game.UserIO.ChessboardData = GameData;
}
