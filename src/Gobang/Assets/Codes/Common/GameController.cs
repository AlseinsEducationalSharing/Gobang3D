abstract class GameController
{
    public static GobangGameController Current { get; set; }

    public void Start()
    {
        InitializeGame();
        do
        {
            SafeThread.HandleAbort();
        }
        while (HandleGame());
        FinalizeGame();
    }

    protected abstract void InitializeGame();
    protected abstract bool HandleGame();
    protected abstract void FinalizeGame();
}
