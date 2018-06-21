using System.Threading.Tasks;

internal abstract class Player
{
    private GameSnapshot _gameData;

    public virtual string Name { get; set; }

    public abstract Task<(int x, int y)> GetNext();

    protected abstract void OnChanged();

    public GameSnapshot GameData
    {
        get => _gameData;
        set
        {
            _gameData = value;
            OnChanged();
        }
    }
}
