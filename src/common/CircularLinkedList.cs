namespace AdventOfCode2015.src.common
{
    public class CircularLinkedList<T>
    {
        private CircularLinkedListNode[] List;

        private int Current = 0;

        public class CircularLinkedListNode
        {
            public CircularLinkedListNode Next { get; set; } = null!;
            public CircularLinkedListNode Previous { get; set; } = null!;
            public T Value { get; set; }
            public CircularLinkedListNode(T Value) { this.Value = Value; }
        }

        public CircularLinkedList(int count) { this.List = new CircularLinkedListNode[count]; }

        public CircularLinkedListNode this[int i]
        {
            get
            {
                return List[i];
            }
        }

        public int Count
        {
            get
            {
                return List.Length;
            }
        }

        public void Add(T value)
        {
            var node = new CircularLinkedListNode(value);

            List[Current] = node;

            if (Current - 1 <= 0 && List[List.Length - 1] != null)
            {
                node.Previous = List[List.Length - 1];
                List[List.Length - 1].Next = node;
            }
            else if (Current >= 1 && List[Current - 1] != null)
            {
                node.Previous = List[Current - 1];
                List[Current - 1].Next = node;
            }

            if (Current + 1 >= List.Length && List[0] != null)
            {
                node.Next = List[0];
                List[0].Previous = node;
            }
            else if (Current < List.Length - 1 && List[Current + 1] != null)
            {
                node.Next = List[Current + 1];
                List[Current + 1].Previous = node;
            }

            Current = (Current + 1) % List.Length;
        }

        public bool Equals(CircularLinkedList<T> other)
        {
            if (other.Count != this.Count) return false;

            var thisStart = 0;
            var thisStartNode = this.List[thisStart];

            var otherStart = 0;
            CircularLinkedListNode otherStartNode = null!;
            for (int i = 0; i < other.Count; i++)
            {
                if (other[i].Value!.Equals(thisStartNode.Value))
                {
                    otherStart = i;
                    otherStartNode = other[i];
                }
            }

            for (int i = 0; i < this.Count; ++i)
            {
                if (!other[(otherStart + i) % this.Count].Value!.Equals(this.List[(thisStart + i) % this.Count].Value))
                {
                    return false;
                }

                thisStart = (thisStart + 1) % this.Count;
                otherStart = (otherStart + 1) % this.Count;
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) { return false; }
            return Equals((CircularLinkedList<T>)obj);
        }

        public override int GetHashCode()
        {
            var first = this.List.OrderBy(x => x.Value).First();
            var firstIndex = Array.IndexOf(this.List, first);
            string hashes = string.Empty;

            for (int i = 0; i < this.Count; i++)
            {
                hashes += this.List[(firstIndex + i) % this.List.Length].Value!.GetHashCode().ToString();
            }

            return hashes.GetHashCode();
        }

        public static bool operator ==(CircularLinkedList<T> left, CircularLinkedList<T> right) => Equals(left, right);
        public static bool operator !=(CircularLinkedList<T> left, CircularLinkedList<T> right) => !Equals(left, right);
    }
}
