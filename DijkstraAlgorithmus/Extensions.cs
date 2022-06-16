namespace DijkstraAlgorithmus
{
    public static class Extensions
    {
        public static void AddBidirectionalEdge(this Graph graph, Node nodeA, Node nodeB, int weight)
        {
            graph.AddEdge(new Edge(nodeA, nodeB, weight));
            graph.AddEdge(new Edge(nodeB, nodeA, weight));
        }
        public static void AddEdge(this Graph graph, Node nodeA, Node nodeB, int weight)
        {
            graph.AddEdge(new Edge(nodeA, nodeB, weight));
        }
    }
}
