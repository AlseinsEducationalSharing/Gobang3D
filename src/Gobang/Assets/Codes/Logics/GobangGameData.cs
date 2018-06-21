using System.Linq;

internal class GobangGameData
{
    public GameSnapshot Current { get; private set; }

    public GobangGameData() => Current = new GameSnapshot();

    private ParallelQuery<(int x, int y)> Directions { get; } = new[] { (1, 0), (0, 1), (1, 1), (1, -1) }.AsParallel();

    private ParallelQuery<int> PosAndNeg { get; } = new[] { -1, 1 }.AsParallel();

    public bool CheckForWin(int x, int y) =>
    (
        from o in Directions
        select
        (
            from j in PosAndNeg
            select
            (
                from i in Enumerable.Range(1, 4)
                select (x: x + o.x * i * j, y: y + o.y * i * j)
            ).TakeWhile(pos =>
                pos.x >= 0 && pos.x < Const.FieldSize.width &&
                pos.y >= 0 && pos.y < Const.FieldSize.height &&
                Current[pos.x, pos.y] == Current.CurrentPlayer
            ).Count()
        ).Sum()
    ).Any(s => s >= 4);

    public bool PositionAvailable((int x, int y) pos) =>
        pos.x >= 0 && pos.x < Const.FieldSize.width &&
        pos.y >= 0 && pos.y < Const.FieldSize.height &&
        !Current.Contains(pos);

    public void Next((int x, int y) pos) => Current += pos;
}
