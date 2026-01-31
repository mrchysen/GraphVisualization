using GraphVisualization.Models;
using SkiaSharp;
using System.Drawing;

namespace GraphVisualization.GraphDrawers;

public class RandomGraphPictureGenerator(
    SKBitmap bitmap,
    Graph graph,
    Rectangle randomZone) 
    : GraphPictureGenerator(bitmap, graph)
{
    public Rectangle RandomZone { get; set; } = randomZone;
    public Random Random { get; set; } = new();
    public Dictionary<int, SKPoint> Points { get; set; } = new();

    public override void Draw()
    {
        Points = new();

        var n = graph.Count;

        for (int i = 0; i < n; i++)
        {
            var point1 = GetPoint(i);

            for (int j = 0; j < graph[i].Count; j++)
            {
                var point2 = GetPoint(graph[i][j].ToNode.Id);
                
                DrawEdge(point1, point2, graph[i][j]);
            }
        }

        for (int i = 0; i < n; i++)
        {
            var point = GetPoint(i);

            DrawNode(i+1, point);
        }
    }

    /// <summary>
    /// Method return random point or point that is in Points
    /// </summary>
    /// <returns></returns>
    protected SKPoint GetPoint(int num) 
    {
        if (Points.ContainsKey(num)) return Points[num];

        var x = Random.Next(RandomZone.Left, RandomZone.Right);
        var y = Random.Next(RandomZone.Top, RandomZone.Bottom);
        
        var point = new SKPoint(x, y);

        int iter = 0;

        while (IsEngaged(point))
        {
            if (iter > 1_000_000)
            {
                Console.WriteLine("Не нашёл");
                break;
            }

            x = Random.Next(RandomZone.Left, RandomZone.Right);
            y = Random.Next(RandomZone.Top, RandomZone.Bottom);

            point = new SKPoint(x, y);

            iter++;
        }

        Points.Add(num, point);

        return point;
    }

    protected bool IsEngaged(SKPoint point)
    {
        var points = Points.Values.ToList();

        for (int i = 0; i < points.Count; i++)
        {
            var point2 = points[i];

            if (Distance(point2, point) < Options.NodeSize.Width * 2 + 1)
            {
                return true;
            }
        }

        return false;
    }

    protected double Distance(SKPoint point1, SKPoint point2)
    {
        return Math.Sqrt(Math.Pow(point1.X - point2.X,2) + Math.Pow(point1.Y - point2.Y, 2));
    }
}
