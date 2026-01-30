namespace GraphVisualization.Models;

public class Graph
{
    public int Count => Nodes.Count;

    public readonly bool IsWeighted = false;

    public readonly bool IsOriented = false;

    public List<Node> Nodes { get; protected set; } = [];

    public Node this[int index] => Nodes[index];

    public Graph() { }

    public Graph(List<Node> graph, bool isWeighted, bool isOriented)
    {
        Nodes = graph;
        IsWeighted = isWeighted;
        IsOriented = isOriented;
    }
}
