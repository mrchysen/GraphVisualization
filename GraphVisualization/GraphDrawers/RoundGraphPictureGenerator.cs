using GraphVisualization.Models;
using SkiaSharp;
using System.Drawing;

namespace GraphVisualization.GraphDrawers;

public class RoundGraphPictureGenerator : GraphPictureGenerator
{
    private int _raduis { get; set; }

    public RoundGraphPictureGenerator(
        SKBitmap bitmap,
        Graph graph, 
        int raduis = 100) : base(bitmap, graph)
    {
        _raduis = raduis;
    }

    public override void Draw()
    {
        var n = _graph.Count;

        var centre = new SKPoint(
            _bitmap.Info.Width / 2 - Options.NodeSize.Width / 2, 
            _bitmap.Info.Height / 2 - Options.NodeSize.Height / 2);
        double fi = 2 * Math.PI / n;

        for (int i = 0; i < n; i++)
        {
            var point = GetPointByNum(i, fi, centre);

            for (int j = 0; j < _graph[i].Count; j++)
            {
                var point2 = GetPointByNum(_graph[i][j].ToNode.Id, fi, centre);
                    
                DrawEdge(point, point2, _graph[i][j]);
            }
        }

        for (int i = 0; i < n; i++)
        {
            var point = GetPointByNum(i, fi, centre);

            DrawNode(i + 1, point);
        }
    }

    protected SKPoint GetPointByNum(int num, double fi, SKPoint centre) => 
        new (
            centre.X + (int)(_raduis * Math.Cos(fi * num)),
            centre.Y + (int)(_raduis * Math.Sin(fi * num)));

}
