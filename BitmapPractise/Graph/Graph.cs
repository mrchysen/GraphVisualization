using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPractise.Graph
{
    public class Graph
    {
        public int Count => Nodes.Count;
        public bool IsOriented => false;
        public List<List<int>> Nodes { get; protected set; } = new List<List<int>>();

        public List<int> this[int index] => Nodes[index];

        public Graph() { }
        public Graph(List<List<int>> graph)
        {
            Nodes = graph;
        }
    }
}
