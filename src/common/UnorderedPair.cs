[Serializable]
public class UnorderedPair<T> : IEquatable<UnorderedPair<T>>
{
    public T? X;
    public T? Y;

    public UnorderedPair()
    {
        X = default(T);
        Y = default(T);
    }

    public UnorderedPair(T x, T y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(UnorderedPair<T> other)
    {
        // if other is null, then it is not equal
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        // if this is the same instance as other, then it is equal
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        //otherwise, compare the hash codes of the two pairs
        return this.GetHashCode() == other.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        // if other is null, then it is not equal
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        // if this is the same instance as other, then it is equal
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        // if other is not of the same type, then it is not equal
        if (obj.GetType() != GetType())
        {
            return false;
        }

        // otherwise, cast other to UnorderedPair<T> and compare
        return Equals((UnorderedPair<T>)obj);
    }

    public override int GetHashCode()
    {
        // and for the HashCode (used as key in HashSet and Dictionary)
        // we actually sort the hash codes of X and Y so it's deterministic
        var hashX = X == null ? 0 : X.GetHashCode();
        var hashY = Y == null ? 0 : Y.GetHashCode();
        return HashCode.Combine(Math.Min(hashX, hashY), Math.Max(hashX, hashY));
    }

    public static bool operator ==(UnorderedPair<T> left, UnorderedPair<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(UnorderedPair<T> left, UnorderedPair<T> right)
    {
        return !Equals(left, right);
    }
}
