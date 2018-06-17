class ManualPlayer : Player
{
    public ManualPlayer()
    {
        UserIO.Initialize();
    }

    public override Point GetNext()
    {
        return UserIO.GetClickPointOnChessboard();
    }

    protected override void OnChanged()
    {
        UserIO.ChessboardData = GameData;
    }
}
