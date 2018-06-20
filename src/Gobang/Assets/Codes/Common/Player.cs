using System.Threading.Tasks;
using Point = Point<int>;

internal abstract class Player
{
    private GameSnapshot _gameData;

    public virtual string Name { get; set; }

    public abstract Task<Point> GetNext();

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
