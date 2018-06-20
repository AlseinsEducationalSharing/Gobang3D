struct Point<T>
{
    public T X { get; private set; }

    public T Y { get; private set; }

    public Point(T X, T Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public override string ToString() => string.Format("({0},{1})", X, Y);

    public override bool Equals(object obj) => obj is Point<T> p && X.Equals(p.X) && Y.Equals(p.Y);

    public override int GetHashCode() => base.GetHashCode();
}
