using System.Linq;

internal class GobangGame
{
    public GameSnapshot Current { get; private set; }

    public GobangGame()
    {
        Current = new GameSnapshot();
    }

    public bool CheckForWin(int x, int y)
    {
        return
             CheckLine(x, y, 1, 0) + CheckLine(x, y, -1, 0) >= 4 ||
             CheckLine(x, y, 0, 1) + CheckLine(x, y, 0, -1) >= 4 ||
             CheckLine(x, y, 1, 1) + CheckLine(x, y, -1, -1) >= 4 ||
             CheckLine(x, y, 1, -1) + CheckLine(x, y, -1, 1) >= 4;
    }

    private int CheckLine(int x, int y, int xo, int yo)
    {
        int i;
        for (i = 0; i < 4; i++)
        {
            x += xo;
            y += yo;
            if
            (
                x < 0 || x >= Const.FieldSize.Width ||
                y < 0 || y >= Const.FieldSize.Height ||
                Current[x, y] != Current.CurrentPlayer
            )
            {
                break;
            }
        }
        return i;
    }

    public bool PositionAvailable(Point pos)
    {
        return
            pos.X >= 0 && pos.X < Const.FieldSize.Width &&
            pos.Y >= 0 && pos.Y < Const.FieldSize.Height &&
            !Current.Contains(pos);
    }

    public void Next(Point pos)
    {
        Current += pos;
    }
}
