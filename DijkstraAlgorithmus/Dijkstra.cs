using System;
using System.Collections.Generic;

namespace DijkstraAlgorithmus
{
    public class Dijkstra
    {
        private class DijkstraNode
        {
            //public List<Edge> Edges { get; set; }
            public Node Node { get; }
            public int Cost { get; set; }
            public bool IsVisited { get; set; }
            public Node Previous { get; set; }
            public DijkstraNode(Node node/*, List<Edge> edges*/)
            {
                //Edges = edges;
                Node = node;
                Cost = int.MaxValue;
                IsVisited = false;
                Previous = null;
            }
        }

        private class DijkstraEdge
        {
            public Edge Edge { get; set; }
            public DijkstraEdge(Edge edge)
            {
                Edge = edge;
            }
        }

        private readonly Graph m_Graph;
        public Dijkstra(Graph graph)
        {
            m_Graph = graph;
        }
        int counter1 = 0;
        int counter2 = 0;
        int counter3 = 0;
        int counter4 = 0;
        int counter5 = 0;
        int counter6 = 0;
        int counter7 = 0;

        public IEnumerable<Node> FindShortestPath(Node startNode, Node targetNode)
        {
            if (!m_Graph.Contains(startNode))
            {
                throw new ArgumentException("Startknoten ist nicht im Graph enthalten.", nameof(startNode));
            }
            if (!m_Graph.Contains(targetNode))
            {
                throw new ArgumentException("Zielknoten ist nicht im Graph enthalten.", nameof(targetNode));
            }

            var dijkstraNodes = new Dictionary<Node, DijkstraNode>();

            DijkstraNode currentNode = null;

            foreach (var node in m_Graph.Nodes)
            {
                //List<Edge> edges = new List<Edge>();
                //foreach (var edge in m_Graph.Edges)
                //{
                //    counter4++;
                //    if (edge.NodeA == node)
                //    {
                //        counter6++;
                //        edges.Add(edge);
                //    }
                //}
                DijkstraNode item = new DijkstraNode(node/*, edges*/);
                if (node == startNode)
                {
                    currentNode = item;
                }
                dijkstraNodes.Add(node, item);
            }

            currentNode.Cost = 0;

            while (currentNode.Node != targetNode)
            {
                counter5++;
                foreach (var e in m_Graph.Edges)
                {
                    counter1++;
                    //Console.WriteLine("Kante:"+e);
                    if (e.NodeA == currentNode.Node)
                    {
                        counter2++;
                        //Console.WriteLine("!!!!!!!!!!!!!!");
                        DijkstraNode dijkstraNode = dijkstraNodes[e.NodeB];
                        if (!dijkstraNode.IsVisited && dijkstraNode.Cost > currentNode.Cost + e.Weight)
                        {
                            //Console.WriteLine("////////////");
                            dijkstraNode.Cost = currentNode.Cost + e.Weight;
                            dijkstraNode.Previous = currentNode.Node;
                        }
                    }
                }
                //foreach (var dijkstraNode in dijkstraNodes.Values)
                //{
                //    counter7++;
                //foreach (var edge in dijkstraNode.Edges)
                //{
                //Console.WriteLine("Edge:" + edge);

                //if (edge.NodeA == currentNode.Node)
                //{
                //if (dijkstraNode.Node == currentNode.Node)
                //{
                //    counter1++;
                //    foreach (var edge in dijkstraNode.Edges)
                //    {
                //        counter2++;
                //        DijkstraNode dijkstraNode2 = dijkstraNodes[edge.NodeB];
                //        if (!dijkstraNode2.IsVisited && dijkstraNode2.Cost > currentNode.Cost + edge.Weight)
                //        {
                //            //Console.WriteLine(":::::::::::::::::::::::::::");
                //            dijkstraNode2.Cost = currentNode.Cost + edge.Weight;
                //            dijkstraNode2.Previous = currentNode.Node;
                //        }
                //    }
                //}
                //}
                //}
                //Console.WriteLine("//////////");
                //}

                //Console.WriteLine("-------------------------------------------------------");
                currentNode.IsVisited = true;
                DijkstraNode next = null;
                foreach (var d in dijkstraNodes.Values)
                {
                    if (!d.IsVisited && d.Cost < (next?.Cost ?? int.MaxValue)) // next == null || next.Cost > d.Cost
                    {
                        next = d;
                    }
                }
                if (next == null || next.Cost == int.MaxValue)
                {
                    throw new Exception("Es gibt keinen Weg vom Start zum Zielknoten!");
                }
                currentNode = next;
                //Console.WriteLine("______---------_______");
            }

            var result = new List<Node>();

            while (currentNode.Node != startNode)
            {
                result.Add(currentNode.Node);
                currentNode = dijkstraNodes[currentNode.Previous];
            }

            result.Add(startNode);
            result.Reverse();
            Console.WriteLine("Counter1:" + counter1 + ", Counter5:" + counter5 + ", Counter2:" + counter2 + ", Counter3:" + counter3 + ", Counter4:" + counter4 + ", Counter6:" + counter6 + ", Counter7:" + counter7);
            return result;
        }
    }
}
