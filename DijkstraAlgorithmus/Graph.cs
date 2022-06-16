using System;
using System.Collections.Generic;

namespace DijkstraAlgorithmus
{
    public class Graph
    {
        public IEnumerable<Node> Nodes => m_Nodes; // { get {return nodes;} }

        public IEnumerable<Edge> Edges => m_Edges; // { get {return edges;} }

        private readonly List<Node> m_Nodes;
        private readonly List<Edge> m_Edges;

        public Graph()
        {
            m_Nodes = new List<Node>();
            m_Edges = new List<Edge>();
        }

        public void AddEdge(Edge e)
        {
            if (m_Nodes.Contains(e.NodeA) && m_Nodes.Contains(e.NodeB))
            {
                m_Edges.Add(e);
            }
            else
            {
                throw new Exception("Die Kante gehört hier nicht rein!");
            }
        }

        public void AddNodes(Node n) => m_Nodes.Add(n);

        public bool Contains(Node n) => m_Nodes.Contains(n);

        public bool AreNeighbours(Node n1, Node n2)
        {
            return (n1.X - n2.X == -1 && n1.Y - n2.Y == 0) ||
                  (n1.X - n2.X == 1 && n1.Y - n2.Y == 0) ||
                  (n1.X - n2.X == 0 && n1.Y - n2.Y == -1) ||
                  (n1.X - n2.X == 0 && n1.Y - n2.Y == 1);
        }

        public  List<Node> GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();
            foreach (var n in Nodes)
            {
                if (AreNeighbours(n, node))
                {
                    neighbours.Add(n);
                }
            }
            return neighbours;
        }
    }
}
