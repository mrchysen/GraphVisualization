using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPractise.Graph;

public class Node
{
    public int Id { get; set; }
    public List<Edge> Edges { get; } = new List<Edge>();

    public int Count => Edges.Count;
    public Edge this[int index] => Edges[index];

    public void Add(Edge edge) => Edges.Add(edge);

    public Node(int id)
    {
        Id = id;
    }
    public Node(int id, List<Edge> edges) : this(id)
    {
        Edges = edges;
    }
}
