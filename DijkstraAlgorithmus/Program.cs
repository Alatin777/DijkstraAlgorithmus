using System;
using System.Linq;

namespace DijkstraAlgorithmus
{
    class Program
    {
            static Graph graph = new Graph();
            public static void Main(string[] args)
            {
                int countID = 0;
                for (int countX = 0; countX < Config.COUNTCOLS; countX++)
                {
                    for (int countY = 0; countY < Config.COUNTROWS; countY++)
                    {
                        graph.AddNodes(new Node(countX, countY, countID));
                        countID++;
                    }
                }

                foreach (var n1 in graph.Nodes)
                {
                    var neighbours = graph.GetNeighbours(n1);
                    foreach (var n2 in neighbours)
                    {
                        graph.AddEdge(n1, n2, 1);
                    }
                } // 7 Sekunden

                Dijkstra d = new Dijkstra(graph);

                Node startNode = graph.Nodes.ElementAt(0);
                Node targetNode = graph.Nodes.ElementAt(Config.COUNTCOLS * Config.COUNTROWS - 1);
                var path = d.FindShortestPath(startNode, targetNode); // 4 Sekunden
                Console.WriteLine("Dijkstra Pfad!");
                foreach (var pathNode in path)
                {
                    Console.WriteLine("Path Node ID: " + pathNode.Id);
                }
                Console.ReadKey();
            }
    }
}
