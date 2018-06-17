abstract class Player
{
    public virtual string Name { get; set; }

    public abstract Point GetNext();

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

    protected bool IsBackgroundWorkingTime
    {
        get
        {
            return UserIO.WaitingForInput;
        }
    }
}
