using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithmus
{
    public class Astar
    {
        public class AstarNode
        {
            public readonly Node Node;
            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost { get; set; }
            public bool IsVisited { get; set; }
            public Node Previous { get; set; }

            public AstarNode(Node node)
            {
                Node = node;
                GCost = int.MaxValue;
                HCost = int.MaxValue;
                FCost = int.MaxValue;
                IsVisited = false;
                Previous = null;
            }

            public void Reset()
            {
                IsVisited = false;
                Previous = null;
                GCost = int.MaxValue;
                HCost = int.MaxValue;
                FCost = int.MaxValue;
            }
        }

        private readonly Graph m_Graph;
        private readonly ILookup<Node, Edge> m_Edges;
        private readonly Dictionary<Node, AstarNode> m_AstarNodes;

        public Astar(Graph graph)
        {
            m_Graph = graph;
            m_AstarNodes = m_Graph.Nodes.ToDictionary(n => n, n => new AstarNode(n));
            m_Edges = m_Graph.Edges.ToLookup(e => e.NodeA);
        }

        private int HeuristicDistance(Node aStarNode1, Node aStarNode2)
        {
            int xDistance = Math.Abs(aStarNode1.X - aStarNode2.X);
            int yDistance = Math.Abs(aStarNode1.Y - aStarNode2.Y);
            return Config.MOVE_STRAIGHT_COST * Math.Min(xDistance,yDistance);
        }

        public IEnumerable<Node> FindShortestPath(Node startNode, Node targetNode)
        {
#if DEBUG
            if (!m_Graph.Contains(startNode))
            {
                throw new ArgumentException("Startknoten ist nicht im Graph enthalten.", nameof(startNode));
            }
            if (!m_Graph.Contains(targetNode))
            {
                throw new ArgumentException("Zielknoten ist nicht im Graph enthalten.", nameof(targetNode));
            }
#endif

            var currentNode = m_AstarNodes[startNode];
            currentNode.GCost = 0;
            currentNode.HCost = HeuristicDistance(startNode,targetNode);
            currentNode.FCost = currentNode.FCost + currentNode.HCost + currentNode.GCost;
            while (currentNode.Node != targetNode)
            {
                foreach (var e in m_Edges[currentNode.Node])
                {
                    var aStarNode = m_AstarNodes[e.NodeB];
                    if (!aStarNode.IsVisited && aStarNode.FCost > currentNode.FCost + e.Weight)
                    {
                        aStarNode.FCost = currentNode.FCost + e.Weight;
                        aStarNode.Previous = currentNode.Node;
                    }
                }

                currentNode.IsVisited = true;
                AstarNode next = null;
                foreach (var d in m_AstarNodes.Values)
                {
                    if (!d.IsVisited && d.FCost < (next?.GCost ?? int.MaxValue))
                    {
                        next = d;
                    }
                }
                if (next == null || next.FCost == int.MaxValue)
                {
                    throw new Exception("Es gibt keinen Weg vom Start zum Zielknoten!");
                }
                currentNode = next;
            }

            var result = new List<Node>();

            while (currentNode.Node != startNode)
            {
                result.Add(currentNode.Node);
                currentNode = m_AstarNodes[currentNode.Previous];
            }
            result.Add(startNode);
            result.Reverse();

            foreach (var dn in m_AstarNodes.Values)
            {
                dn.Reset();
            }

            return result;
        }
    }
}
