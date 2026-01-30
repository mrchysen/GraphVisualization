namespace GraphVisualization.Models;

public class Edge(Node toNode, int weight = 1)
{
    public Node ToNode { get; set; } = toNode;
    public int Weight { get; set; } = weight;
}
