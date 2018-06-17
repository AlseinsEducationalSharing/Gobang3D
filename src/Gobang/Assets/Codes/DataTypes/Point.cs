internal struct Point
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Point(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public override string ToString()
    {
        return string.Format("({0},{1})", X, Y);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Point))
        {
            return false;
        }
        Point p = (Point)obj;
        return p.X == X && p.Y == Y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

struct Point<T>
{
    public T X { get; private set; }
    public T Y { get; private set; }
    public Point(T X, T Y)
    {
        this.X = X;
        this.Y = Y;
    }
    public override string ToString()
    {
        return string.Format("({0},{1})", X, Y);
    }
}
