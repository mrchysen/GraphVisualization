using GraphVisualization.Models;
using SkiaSharp;

namespace GraphVisualization.GraphRenderer;

public class TreeRenderer : GraphRenderer
{
    private List<DepthNodes> _helpStructure = null!;

    public TreeRenderer(
        SKBitmap bitmap, 
        Graph graph, 
        RendererOptions? options = null) : base(bitmap, graph, options)
    {
        if (!graph.IsTree)
        {
            throw new ArgumentException("Graph is not a tree");
        }

        UpdateHelpStructure();
    }

    public override void Render()
    {
        var centre = new SKPoint(
            _bitmap.Info.Width / 2,
            0);

        float deltaHeight = 100;
        float heightStart = _bitmap.Info.Width / 2 - _helpStructure.Count * deltaHeight / 2;

        // Arrows
        for (int i = 0; i < _helpStructure.Count - 1; i++)
        {
            var fatherDepth = _helpStructure[i].Depth;
            var fatherDepthDeltaWidth = _bitmap.Info.Width / (_helpStructure[i].Nodes.Count + 1);

            var childrenDepth = _helpStructure[i + 1].Depth;
            var childrenDepthDeltaWidth = _bitmap.Info.Width / (_helpStructure[i + 1].Nodes.Count + 1);

            foreach (var fatherNode in _helpStructure[i].Nodes)
            {
                var farherNum = _helpStructure[i].Nodes.IndexOf(fatherNode);

                SKPoint fatherPoint =
                    new SKPoint(fatherDepthDeltaWidth * (farherNum + 1), heightStart + deltaHeight * fatherDepth);

                foreach (var childrenNode in fatherNode.Edges.Select(x => x.ToNode))
                {
                    var chidlrenNum = _helpStructure[i + 1].Nodes.IndexOf(childrenNode);

                    SKPoint childrenPoint =
                        new SKPoint(childrenDepthDeltaWidth * (chidlrenNum + 1), heightStart + deltaHeight * childrenDepth);

                    DrawEdge(fatherPoint, childrenPoint);
                }
            }
        }

        // Nodes
        foreach (DepthNodes node in _helpStructure) 
        {
            var current = node.Depth;

            var depthDeltaWidth = _bitmap.Info.Width / (node.Nodes.Count + 1);

            for (var i = 0; i < node.Nodes.Count; i++) 
            {
                DrawNode(node.Nodes[i].Id, new SKPoint(depthDeltaWidth * (i + 1), heightStart + deltaHeight * current));
            }
        }
    }

    private void UpdateHelpStructure() 
    {
        var maxNode = _graph.Nodes.MaxBy(x => x.Edges.Count) ?? 
            throw new InvalidOperationException("Unable to find max count edge node");

        var s = DepthSearch(maxNode).ToList();

        _helpStructure = DepthSearch(maxNode)
            .GroupBy((n) => n.Depth)
            .Select(x => 
                new DepthNodes(
                    x.Key, 
                x.OrderBy(x => x.NodeIdParent).Select(n => n.Node).ToList()))
            .OrderBy(x => x.Depth)
            .ToList();
    }

    private IEnumerable<NodeDepth> DepthSearch(Node node, int NodeIdParent = -1, int depthLevel = 1)
    {
        foreach (var edge in node.Edges) 
        {
            foreach(var depth in DepthSearch(edge.ToNode, node.Id, depthLevel + 1))
            {
                yield return depth;
            }
        }

        yield return new(node, depthLevel, NodeIdParent);
    }

    private record NodeDepth(Node Node, int Depth, int NodeIdParent);
    private record DepthNodes(int Depth, List<Node> Nodes);
}
