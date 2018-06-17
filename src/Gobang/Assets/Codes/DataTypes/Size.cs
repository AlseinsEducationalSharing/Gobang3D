struct Size
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Size(int X, int Y)
    {
        this.Width = X;
        this.Height = Y;
    }
}

struct Size<T>
{
    public T Width { get; private set; }
    public T Height { get; private set; }
    public Size(T X, T Y)
    {
        this.Width = X;
        this.Height = Y;
    }
}
