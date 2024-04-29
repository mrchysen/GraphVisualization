using System.Xml.Linq;

namespace BitmapPractise.Graph;

public class Edge
{
    public Node ToNode { get; set; }
    public int Weight { get; set; } = 1;

    public Edge(Node toNode, int weight = 1)
    {
        Weight = weight;
        ToNode = toNode;
    }
}
