public struct Rect2I
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public int Area { get { return X * Y; } }

    public Vector2I Min { get { return new Vector2I(X, Y); } }
    public Vector2I Max { get { return new Vector2I(X + Width, Y + Height); } }

    public Rect2I(int X, int Y, int Width, int Height)
    {
        this.X = X;
        this.Y = Y;
        this.Width = Width;
        this.Height = Height;
    }

    public bool Overlaps(Rect2I other)
    {
        return this.Min.X < other.Max.X &&
            this.Min.Y < other.Max.Y &&
            this.Max.X > other.Min.X &&
            this.Max.Y > other.Min.Y;
    }

    public Rect2I GetOverlap(Rect2I other)
    {
        //                         Max (0, 7 - 5)
        int xOverlap = Math.Max(0, Math.Min(this.Max.X, other.Max.X) - Math.Max(this.Min.X, other.Min.X));
        ConsoleEx.Green($"Overlap Width X: {xOverlap}");
        //                         Max (0, 6 - 4)
        int yOverlap = Math.Max(0, Math.Min(this.Max.Y, other.Max.Y) - Math.Max(this.Min.Y, other.Min.Y));
        ConsoleEx.Green($"Overlap Width Y: {yOverlap}");

        int xPosMin = Math.Max(this.Min.X, other.Min.X);
        int yPosMin = Math.Max(this.Min.Y, other.Min.Y);

        int xPosMax = Math.Min(this.Max.X, other.Max.X);
        int yPosMax = Math.Min(this.Max.Y, other.Max.Y);

        Rect2I overlapRect = new Rect2I(xPosMin, yPosMin, xPosMax - xPosMin, yPosMax - yPosMin);

        return overlapRect;
    }

    public override string ToString()
    {
        return $"Pos[{X},{Y}] Size[{Width},{Height}] Area[{Area}] Min[{Min}] Max[{Max}]";
    }
}