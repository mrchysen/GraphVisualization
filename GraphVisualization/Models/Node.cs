namespace GraphVisualization.Models;

public class Node
{
    public int Id { get; set; }

    public List<Edge> Edges { get; } = [];

    public int Count => Edges.Count;

    public Edge this[int index] => Edges[index];

    public void Add(Edge edge) => Edges.Add(edge);

    public Node(int id)
    {
        Id = id;
    }

    public Node(int id, List<Edge>? edges = null) : this(id)
    {
        Edges = edges ?? [];
    }
}
