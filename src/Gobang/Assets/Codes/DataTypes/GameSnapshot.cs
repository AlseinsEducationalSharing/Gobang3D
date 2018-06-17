using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class GameSnapshot : IEnumerable<Point>
{
    private GameSnapshot Previous;
    private Point StepData;

    public int StepCount { get; private set; }

    public Faction this[int x, int y]
    {
        get
        {
            GameSnapshot target = this;
            while (target.StepCount > 0)
            {
                if (target.StepData.X == x && target.StepData.Y == y)
                {
                    return target.CurrentPlayer;
                }
                target = target.Previous;
            }
            return Faction.None;
        }
    }

    public Faction this[Point pos]
    {
        get
        {
            return this[pos.X, pos.Y];
        }
    }

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
                target = target.Previous;
            }
            return target.StepData;
        }
    }

    public static GameSnapshot operator +(GameSnapshot Previous, Point StepData)
    {
        return new GameSnapshot(Previous, StepData);
    }

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
            result.Add(Current.StepData);
            Current = Current.Previous;
        }
        if (Current != Previous)
        {
            return null;
        }
        return result.ToArray();
    }

    public static bool operator ==(GameSnapshot This, GameSnapshot Other)
    {
        return ReferenceEquals(This, Other) || This.Equals(Other);
    }

    public static bool operator !=(GameSnapshot This, GameSnapshot Other)
    {
        return !ReferenceEquals(This, Other) && !This.Equals(Other);
    }

    public override bool Equals(object obj)
    {
        var other = obj as GameSnapshot;
        return other != null && (other.StepCount == 0 && StepCount == 0 || other.StepData.Equals(StepData) && other.Previous == Previous);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public GameSnapshot()
    {
        StepCount = 0;
    }

    private GameSnapshot(GameSnapshot Previous, Point StepData)
    {
        this.Previous = Previous;
        this.StepData = StepData;
        StepCount = Previous.StepCount + 1;
    }

    public Faction CurrentPlayer
    {
        get
        {
            return StepCount % 2 == 1 ? Faction.Black : Faction.White;
        }
    }

    public Faction NextPlayer
    {
        get
        {
            return StepCount % 2 == 0 ? Faction.Black : Faction.White;
        }
    }

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
                result[target.StepData.X, target.StepData.Y] = target.CurrentPlayer;
                target = target.Previous;
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
            result[i] = target.StepData;
            target = target.Previous;
        }
        return ((IEnumerable<Point>)result).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
