using System.Threading.Tasks;

internal abstract class GameWorkflow
{
    public async Task Start()
    {
        InitializeGame();
        while (await HandleGame()) ;
        FinalizeGame();
    }

    protected abstract void InitializeGame();
    protected abstract Task<bool> HandleGame();
    protected abstract void FinalizeGame();
}
