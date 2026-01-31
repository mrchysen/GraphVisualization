namespace GraphVisualization.Models;

public class Graph
{
    private List<Node> _nodes;

    public int Count => _nodes.Count;

    public readonly bool IsWeighted = false;

    public readonly bool IsOriented = false;

    public IEnumerable<Node> Nodes => _nodes;

    public Node this[int index] => _nodes[index];

    public Graph(List<Node> graph, bool isWeighted, bool isOriented)
    {
        _nodes = graph;
        IsWeighted = isWeighted;
        IsOriented = isOriented;
    }
}
