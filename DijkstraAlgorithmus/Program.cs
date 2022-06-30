using System;
using System.Linq;

namespace DijkstraAlgorithmus
{
    internal class Program
    {
        private static readonly Graph s_Graph = new();

        public static void Main() {
            var sw = new System.Diagnostics.Stopwatch();

            // Setup
            sw.Start();

            var countId = 0;
            var tempNodes = new Node[Config.COUNT_COLS, Config.COUNT_ROWS];

            for (var countX = 0; countX < Config.COUNT_COLS; countX++) {
                for (var countY = 0; countY < Config.COUNT_ROWS; countY++) {
                    var node = new Node(countX, countY, countId);
                    tempNodes[countX, countY] = node;
                    s_Graph.AddNodes(node);
                    countId++;
                }
            }

            // Horizontale Edges
            for (var y = 0; y < Config.COUNT_ROWS; ++y) {
                for (var x = 0; x < Config.COUNT_COLS - 1; ++x) {
                    s_Graph.AddEdge(tempNodes[x, y], tempNodes[x + 1, y], 1);
                    s_Graph.AddEdge(tempNodes[x + 1, y], tempNodes[x, y], 1);
                }
            }

            // Vertikale Edges
            for (var x = 0; x < Config.COUNT_COLS; ++x) {
                for (var y = 0; y < Config.COUNT_ROWS - 1; ++y) {
                    s_Graph.AddEdge(tempNodes[x, y], tempNodes[x, y + 1], 1);
                    s_Graph.AddEdge(tempNodes[x, y + 1], tempNodes[x, y], 1);
                }
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            // Create
            var d = new Dijkstra(s_Graph);
            var astar = new Astar(s_Graph);

            // 3.6 Sekunden (Ursprung)
            // 2.2 Sekunden (Readonly Struct)
            // 2.4 Sekunden (Readonly Class)
            // 1.3 Sekunden (tempNodes statt GetNeighbours)

            // 1323 Millisekunden (Debug)
            //    8 Millisekunden (Release)


            // Execute
            sw.Restart();
            var startNode = s_Graph.Nodes[0];
            var targetNode = s_Graph.Nodes[Config.COUNT_COLS * Config.COUNT_ROWS - 1];
            var astarPath = astar.FindShortestPath(startNode, targetNode);
            // 438 Millisekunden (Release)
            Console.WriteLine("Astar:" + sw.Elapsed);
            Console.WriteLine(astarPath.Count());
            sw.Stop();
            sw.Restart();
            var dijkstraPath = d.FindShortestPath(startNode, targetNode);
            sw.Stop();
            Console.WriteLine("Dijkstra:"+sw.Elapsed);
            Console.WriteLine(dijkstraPath.Count());
            // 5.5 Sekunden (Ursprung Debug)
            // 3.1 Sekunden (Ursprung Release)

            // 859 Millisekunden (Debug)
            // 400 Millisekunden (Release)
            Console.ReadKey();
        }
    }
}
