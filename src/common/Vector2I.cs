public struct Vector2I
{
    public int X { get; set; }
    public int Y { get; set; }
    public Vector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static Vector2I operator +(Vector2I left, Vector2I right)
    {
        left.X += right.X;
        left.Y += right.Y;
        return left;
    }

    public static Vector2I operator -(Vector2I left, Vector2I right)
    {
        left.X -= right.X;
        left.Y -= right.Y;
        return left;
    }

    public static Vector2I operator *(Vector2I left, float right)
    {
        left.X = (int)Math.Round(left.X * right);
        left.Y = (int)Math.Round(left.Y * right);
        return left;
    }

    public static Vector2I operator *(Vector2I left, int right)
    {
        left.X *= right;
        left.Y *= right;
        return left;
    }

    public static Vector2I operator /(Vector2I left, int right)
    {
        if (right == 0) throw new DivideByZeroException("Cannot divide by zero.");
        left.X /= right;
        left.Y /= right;
        return left;
    }

    public static Vector2I operator /(Vector2I left, float right)
    {
        if (right == 0) throw new DivideByZeroException("Cannot divide by zero.");
        left.X = (int)Math.Round(left.X / right);
        left.Y = (int)Math.Round(left.Y / right);
        return left;
    }

    public static bool operator ==(Vector2I left, Vector2I right)
    {
        return left.X == right.X && left.Y == right.Y;
    }

    public static bool operator !=(Vector2I left, Vector2I right)
    {
        return !(left == right);
    }

    //public static Vector2I Min(Vector2I left, Vector2I right)
    //{
    //    return new Vector2I();
    //}

    public override readonly bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Vector2I))
        {
            return false;
        }
        return this == (Vector2I)obj;
    }

    //check this implementation later ripped from stack overflow
    //not sure it actually works <.< 
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = (int)2166136261;
            // Suitable nullity checks etc, of course :)
            hash = (hash * 16777619) ^ X.GetHashCode();
            hash = (hash * 16777619) ^ Y.GetHashCode();
            return hash;
        }
    }
}
