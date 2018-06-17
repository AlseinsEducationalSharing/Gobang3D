using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

internal class UserIO
{
    private Game _game;

    public UserIO(Game game) => _game = game;

    private GameObject ChessCollection => _game.Chessboard.transform.GetChild(0).gameObject;
    private GameObject ChessBlocks => _game.Chessboard.transform.GetChild(2).gameObject;

    private GameObject BlackChess, WhiteChess;

    private bool IsInitialized = false;
    public void Initialize()
    {
        if (!IsInitialized)
        {
            BlackChess = (GameObject)Resources.Load("Items/Black");
            WhiteChess = (GameObject)Resources.Load("Items/White");
            IsInitialized = true;
        }
    }

    private GameSnapshot chessboardData = new GameSnapshot();
    public GameSnapshot ChessboardData
    {
        set
        {
            if (value == new GameSnapshot())
            {
                for (var i = ChessCollection.transform.childCount - 1; i >= 0; i--)
                {
                    Object.Destroy(ChessCollection.transform.GetChild(i).gameObject);
                }
                return;
            }
            var dif = value - chessboardData;
            if (dif == null)
            {
                ChessboardData = new GameSnapshot();
                dif = value.ToArray();
            }
            var map = value.Map;
            foreach (var item in dif)
            {
                var newChess = Object.Instantiate(map[item.X, item.Y] == Faction.Black ? BlackChess : WhiteChess);
                newChess.transform.parent = ChessCollection.transform;
                newChess.transform.localPosition = new Vector3((7 - item.X) * Const.UnitSize, 0, (item.Y - 7) * Const.UnitSize);
            }
            chessboardData = value;
        }
    }

    public async Task<Point> GetClickPointOnChessboard() => await ChessBlocks.GetComponent<Chessboard>().Result;
}
