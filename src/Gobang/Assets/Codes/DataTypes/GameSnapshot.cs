using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Point = Point<int>;

internal class GameSnapshot : IEnumerable<Point>
{
    private GameSnapshot _previous;
    private Point _stepData;

    public int StepCount { get; private set; }

    public Faction this[int x, int y]
    {
        get
        {
            GameSnapshot target = this;

            while (target.StepCount > 0)
            {
                if (target._stepData.X == x && target._stepData.Y == y)
                {
                    return target.CurrentPlayer;
                }
                target = target._previous;
            }

            return Faction.None;
        }
    }

    public Faction this[Point pos] => this[pos.X, pos.Y];

    public Point this[int step]
    {
        get
        {
            if (step >= StepCount || step <= 0)
            {
                throw new IndexOutOfRangeException();
            }

            var target = this;

            for (int i = step; i < StepCount; i++)
            {
                target = target._previous;
            }

            return target._stepData;
        }
    }

    public static GameSnapshot operator +(GameSnapshot previous, Point stepData) => new GameSnapshot(previous, stepData);

    public static Point[] operator -(GameSnapshot Current, GameSnapshot Previous)
    {
        if (Previous.StepCount == 0)
        {
            return Current.ToArray();
        }

        if (Previous.StepCount > Current.StepCount)
        {
            return null;
        }

        var result = new List<Point>();

        while (Current.StepCount > Previous.StepCount)
        {
            result.Add(Current._stepData);
            Current = Current._previous;
        }

        if (Current != Previous)
        {
            return null;
        }

        return result.ToArray();
    }

    public static bool operator ==(GameSnapshot @this, GameSnapshot other) => ReferenceEquals(@this, other) || !(@this is null) && @this.Equals(other);

    public static bool operator !=(GameSnapshot @this, GameSnapshot other) => !(@this == other);

    public override bool Equals(object obj)
    {
        var other = obj as GameSnapshot;
        return !(other is null) && (other.StepCount == 0 && StepCount == 0 || other._stepData.Equals(_stepData) && other._previous == _previous);
    }

    public override int GetHashCode() => base.GetHashCode();

    public GameSnapshot() => StepCount = 0;

    private GameSnapshot(GameSnapshot previous, Point stepData)
    {
        _previous = previous;
        _stepData = stepData;
        StepCount = previous.StepCount + 1;
    }

    public Faction CurrentPlayer => StepCount % 2 == 1 ? Const.FirstPlayer : Const.FirstPlayer.GetOpponent();

    public Faction NextPlayer => CurrentPlayer.GetOpponent();

    public Faction[,] Map
    {
        get
        {
            var result = new Faction[Const.FieldSize.Width, Const.FieldSize.Height];

            for (var i = 0; i < Const.FieldSize.Width; i++)
            {
                for (var j = 0; j < Const.FieldSize.Height; j++)
                {
                    result[i, j] = Faction.None;
                }
            }

            GameSnapshot target = this;

            while (target.StepCount > 0)
            {
                result[target._stepData.X, target._stepData.Y] = target.CurrentPlayer;
                target = target._previous;
            }

            return result;
        }
    }

    public IEnumerator<Point> GetEnumerator()
    {
        GameSnapshot target = this;
        Point[] result = new Point[StepCount];

        for (int i = StepCount - 1; i >= 0; i--)
        {
            result[i] = target._stepData;
            target = target._previous;
        }

        return ((IEnumerable<Point>)result).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
