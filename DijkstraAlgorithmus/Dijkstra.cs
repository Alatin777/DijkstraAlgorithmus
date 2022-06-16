using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraAlgorithmus
{
    public class Dijkstra
    {
        private class DijkstraNode
        {
            public readonly Node Node;

            public int Cost { get; set; }
            public bool IsVisited { get; set; }
            public Node Previous { get; set; }

            public DijkstraNode(Node node) {
                Node = node;
                Cost = int.MaxValue;
                IsVisited = false;
                Previous = null;
            }

            public void Reset() {
                IsVisited = false;
                Previous = null;
                Cost = int.MaxValue;
            }
        }

        private readonly Graph m_Graph;
        private readonly ILookup<Node, Edge> m_Edges;
        private readonly Dictionary<Node, DijkstraNode> m_DijkstraNodes;

        public Dijkstra(Graph graph) {
            m_Graph = graph;
            m_DijkstraNodes = m_Graph.Nodes.ToDictionary(n => n, n => new DijkstraNode(n));
            m_Edges = m_Graph.Edges.ToLookup(e => e.NodeA);
        }

        public IEnumerable<Node> FindShortestPath(Node startNode, Node targetNode) {
#if DEBUG
            if (!m_Graph.Contains(startNode)) {
                throw new ArgumentException("Startknoten ist nicht im Graph enthalten.", nameof(startNode));
            }
            if (!m_Graph.Contains(targetNode)) {
                throw new ArgumentException("Zielknoten ist nicht im Graph enthalten.", nameof(targetNode));
            }
#endif

            var currentNode = m_DijkstraNodes[startNode];
            currentNode.Cost = 0;

            while (currentNode.Node != targetNode) {
                foreach (var e in m_Edges[currentNode.Node]) {
                    var dijkstraNode = m_DijkstraNodes[e.NodeB];
                    if (!dijkstraNode.IsVisited && dijkstraNode.Cost > currentNode.Cost + e.Weight) {
                        dijkstraNode.Cost = currentNode.Cost + e.Weight;
                        dijkstraNode.Previous = currentNode.Node;
                    }
                }

                currentNode.IsVisited = true;
                DijkstraNode next = null;
                foreach (var d in m_DijkstraNodes.Values) {
                    if (!d.IsVisited && d.Cost < (next?.Cost ?? int.MaxValue)) {
                        next = d;
                    }
                }
                if (next == null || next.Cost == int.MaxValue) {
                    throw new Exception("Es gibt keinen Weg vom Start zum Zielknoten!");
                }
                currentNode = next;
            }

            var result = new List<Node>();

            while (currentNode.Node != startNode) {
                result.Add(currentNode.Node);
                currentNode = m_DijkstraNodes[currentNode.Previous];
            }
            result.Add(startNode);
            result.Reverse();

            foreach (var dn in m_DijkstraNodes.Values) {
                dn.Reset();
            }

            return result;
        }
    }
}
