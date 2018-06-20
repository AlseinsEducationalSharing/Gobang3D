internal struct Size<T>
{
    public T Width { get; private set; }

    public T Height { get; private set; }

    public Size(T X, T Y)
    {
        Width = X;
        Height = Y;
    }

    public override string ToString() => string.Format("({0},{1})", Width, Height);

    public override bool Equals(object obj) => obj is Size<T> p && Width.Equals(p.Width) && Height.Equals(p.Height);

    public override int GetHashCode() => base.GetHashCode();
}
