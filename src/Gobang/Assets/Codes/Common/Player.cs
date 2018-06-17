using System.Threading.Tasks;

internal abstract class Player
{
    public virtual string Name { get; set; }

    public abstract Task<Point> GetNext();

    protected abstract void OnChanged();

    private GameSnapshot gameData;
    public GameSnapshot GameData
    {
        get
        {
            return gameData;
        }
        set
        {
            gameData = value;
            OnChanged();
        }
    }
}
