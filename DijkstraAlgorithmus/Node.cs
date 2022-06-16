using System.Collections.Generic;

namespace DijkstraAlgorithmus
{
    public class Node
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public Node(int x, int y, int id)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public override string ToString()
        {
            return "ID: " + Id;
        }
    }
}
