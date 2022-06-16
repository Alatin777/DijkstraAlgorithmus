using System;

namespace DijkstraAlgorithmus
{
    public class Node : IEquatable<Node>
    {
        public readonly int Id;
        public readonly int X;
        public readonly int Y;

        public Node(int x, int y, int id) {
            X = x;
            Y = y;
            Id = id;
        }

        public static bool operator ==(Node left, Node right) => left.Id == right.Id;

        public static bool operator !=(Node left, Node right) => !(left == right);

        public override string ToString() => "ID: " + Id;

        public override bool Equals(object obj) => obj is Node node && Equals(node);

        public override int GetHashCode() => Id;

        public bool Equals(Node other) => Id == other.Id;
    }
}
