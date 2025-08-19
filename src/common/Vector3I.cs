public struct Vector3I
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public Vector3I(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
    public static Vector3I operator +(Vector3I left, Vector3I right)
    {
        left.X += right.X;
        left.Y += right.Y;
        left.Z += right.Z;
        return left;
    }
    public static Vector3I operator -(Vector3I left, Vector3I right)
    {
        left.X -= right.X;
        left.Y -= right.Y;
        left.Z -= right.Z;
        return left;
    }
    public static Vector3I operator *(Vector3I left, float right)
    {
        left.X = (int)Math.Round(left.X * right);
        left.Y = (int)Math.Round(left.Y * right);
        left.Z = (int)Math.Round(left.Z * right);
        return left;
    }
    public static Vector3I operator *(Vector3I left, int right)
    {
        left.X *= right;
        left.Y *= right;
        left.Z *= right;
        return left;
    }
    public static Vector3I operator /(Vector3I left, int right)
    {
        if (right == 0) throw new DivideByZeroException("Cannot divide by zero.");
        left.X /= right;
        left.Y /= right;
        left.Z /= right;
        return left;
    }
    public static Vector3I operator /(Vector3I left, float right)
    {
        if (right == 0) throw new DivideByZeroException("Cannot divide by zero.");
        left.X = (int)Math.Round(left.X / right);
        left.Y = (int)Math.Round(left.Y / right);
        left.Z = (int)Math.Round(left.Z / right);
        return left;
    }

    public static bool operator ==(Vector3I left, Vector3I right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    }

    public static bool operator !=(Vector3I left, Vector3I right)
    {
        return !(left == right);
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Vector3I))
        {
            return false;
        }
        return this == (Vector3I)obj;
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
            hash = (hash * 16777619) ^ Z.GetHashCode();
            return hash;
        }
    }
}