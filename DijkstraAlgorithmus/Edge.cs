using System;

namespace DijkstraAlgorithmus
{
    public class Edge : IEquatable<Edge>
    {
        public Node NodeA { get; }

        public Node NodeB { get; }

        public int Weight { get; }

        public Edge(Node nodeA, Node nodeB, int weight) {
            if (nodeA == nodeB) {
                throw new Exception("Es wurde versucht eine Edge mit selber Child- und Parentnode zu erzeugen!");
            }
            NodeA = nodeA;
            NodeB = nodeB;
            Weight = weight;
        }

        public bool Equals(Edge other) =>
            other != null &&
            other.NodeA.Equals(NodeA) &&
            other.NodeB.Equals(NodeB) &&
            other.Weight == Weight;

        public override bool Equals(object obj) => Equals(obj as Edge);

        public override string ToString() => "ParentNode:" + NodeA + ", ChildNode" + NodeB + ", Weight:" + Weight;

        public override int GetHashCode() => HashCode.Combine(NodeA, NodeB);
    }
}
