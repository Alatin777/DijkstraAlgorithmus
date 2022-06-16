using System.Collections.Generic;

namespace DijkstraAlgorithmus
{
    public class Graph
    {
        public IReadOnlyList<Node> Nodes => m_Nodes; // { get {return nodes;} }

        public IReadOnlyCollection<Edge> Edges => m_Edges; // { get {return edges;} }

        private readonly List<Node> m_Nodes;
        private readonly List<Edge> m_Edges;

        public Graph() {
            m_Nodes = new List<Node>();
            m_Edges = new List<Edge>();
        }

#if DEBUG
        public void AddEdge(Edge e) {
            if (m_Nodes.Contains(e.NodeA) && m_Nodes.Contains(e.NodeB)) {
                m_Edges.Add(e);
            } else {
                throw new System.Exception("Die Kante gehört hier nicht rein!");
            }
        }

        public bool Contains(Node n) => m_Nodes.Contains(n);
#else
        public void AddEdge(Edge e) => m_Edges.Add(e);
#endif

        public void AddNodes(Node n) => m_Nodes.Add(n);
    }
}
