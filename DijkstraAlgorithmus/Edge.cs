using System;

namespace DijkstraAlgorithmus
{
    public class Edge
    {
        public Node NodeA { get; }
        public Node NodeB { get; }
        public int Weight { get; }
        public Edge(Node nodeA, Node nodeB, int weight)
        {
            if (nodeA == nodeB)
            {
                throw new Exception("Es wurde versucht eine Edge mit selber Child- und Parentnode zu erzeugen!");
            }
            NodeA = nodeA;
            NodeB = nodeB;
            Weight = weight;
        }

        public override bool Equals(object obj)
        {
            Edge edge2 = (Edge)obj;
            // Prüfe, ob wir zwei identische Edges mit der selben Reihenfolgen haben A -> B     A -> B
            if (edge2.NodeA == NodeA && edge2.NodeB == NodeB)
            {
                return true;
            }
            // Prüfe, ob wir zwei idetische Edges mit gespiegelter Reihenfolgen haben A -> B     B -> A
            if (edge2.NodeA == NodeB && edge2.NodeB == NodeA)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "ParentNode:" + NodeA + ", ChildNode" + NodeB + ", Weight:" + Weight;
        }
    }
}
