using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

internal class UserIO
{
    private Game _game;
    private GameSnapshot _chessboardData = new GameSnapshot();
    private GameObject _blackChess, _whiteChess;
    private bool _isInitialized = false;

    private GameObject ChessCollection => _game.Chessboard.transform.GetChild(0).gameObject;
    private GameObject ChessBlocks => _game.Chessboard.transform.GetChild(2).gameObject;

    public UserIO(Game game) => _game = game;

    public void Initialize()
    {
        if (!_isInitialized)
        {
            _blackChess = (GameObject)Resources.Load("Items/Black");
            _whiteChess = (GameObject)Resources.Load("Items/White");
            _isInitialized = true;
        }
    }

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

            var dif = value - _chessboardData;

            if (dif == null)
            {
                ChessboardData = new GameSnapshot();
                dif = value.ToArray();
            }

            var map = value.Map;

            foreach (var item in dif)
            {
                var newChess = Object.Instantiate(map[item.x, item.y] == Faction.Black ? _blackChess : _whiteChess);
                newChess.transform.parent = ChessCollection.transform;
                newChess.transform.localPosition = new Vector3((7 - item.x) * Const.UnitSize, 0, (item.y - 7) * Const.UnitSize);
            }

            _chessboardData = value;
        }
    }

    public async Task<(int x, int y)> GetClickPoint() => await ChessBlocks.GetComponent<Chessboard>().Result;
}
