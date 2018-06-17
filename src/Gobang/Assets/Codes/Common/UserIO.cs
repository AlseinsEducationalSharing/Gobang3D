using System.Linq;
using UnityEngine;

static class UserIO
{
    static UserIO()
    {
        WaitingForInput = false;
    }

    private static GameObject BlackChess, WhiteChess;

    private static bool IsInitialized = false;
    public static void Initialize()
    {
        if (!IsInitialized)
        {
            MapInvoker.Invoke(target =>
            {
                BlackChess = (GameObject)Resources.Load("Items/Black");
                WhiteChess = (GameObject)Resources.Load("Items/White");
            });
            IsInitialized = true;
        }
    }

    private static GameSnapshot chessboardData = new GameSnapshot();
    public static GameSnapshot ChessboardData
    {
        set
        {
            if (value == new GameSnapshot())
            {
                MapInvoker.Invoke(target =>
                {
                    for (var i = target.transform.childCount - 1; i >= 0; i--)
                    {
                        UnityEngine.Object.Destroy(target.transform.GetChild(i).gameObject);
                    }
                });
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
                MapInvoker.Invoke(target =>
                {
                    var newChess = UnityEngine.Object.Instantiate(map[item.X, item.Y] == Faction.Black ? BlackChess : WhiteChess);
                    newChess.transform.parent = target.transform;
                    newChess.transform.localPosition = new Vector3((7 - item.X) * Const.UnitSize, 0, (item.Y - 7) * Const.UnitSize);
                });
            }
            chessboardData = value;
        }
    }

    public static bool WaitingForInput { get; private set; }

    public static Point GetClickPointOnChessboard()
    {
        try
        {
            WaitingForInput = true;
            lock (Chessboard.ClickResult)
            {
                do
                {
                    SafeThread.HandleAbort();
                } while (Chessboard.ClickResult.Count == 0);
                return Chessboard.ClickResult.Dequeue();
            }
        }
        finally
        {
            WaitingForInput = false;
        }
    }

    public static Invoker<GameObject> MapInvoker { get; set; }
}
