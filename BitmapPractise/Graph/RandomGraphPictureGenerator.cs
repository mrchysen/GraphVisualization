using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPractise.Graph;

public class RandomGraphPictureGenerator : GraphPictureGenerator
{
    public Random Random { get; set; } = new();
    public Rectangle MapZone { get; set; } = new();
    public Dictionary<int, Point> Points { get; set; } = new();

    public RandomGraphPictureGenerator(Bitmap picture, Graphics graphics, Graph graph, Rectangle rectangle) : base(picture, graphics, graph)
    {
        MapZone = rectangle;
    }

    protected override void DrawGraph()
    {
        var n = graph.Count;

        for (int i = 0; i < n; i++)
        {
            var point1 = GetPoint(i);

            for (int j = 0; j < graph[i].Count; j++)
            {
                var point2 = GetPoint(graph[i][j]);
                
                DrawEdge(point1, point2);
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
    protected Point GetPoint(int num) 
    {
        if (Points.ContainsKey(num)) return Points[num];

        var x = Random.Next(MapZone.Left, MapZone.Right);
        var y = Random.Next(MapZone.Top, MapZone.Bottom);
        
        var point = new Point(x, y);

        int iter = 0;

        while (IsEngaged(point))
        {
            if (iter > 1000)
            {
                Console.WriteLine("Не нашёл");
                break;
            }
                

            x = Random.Next(MapZone.Left, MapZone.Right);
            y = Random.Next(MapZone.Top, MapZone.Bottom);

            point = new Point(x, y);

            iter++;
        }

        Points.Add(num, point);

        return point;
    }

    protected bool IsEngaged(Point point)
    {
        var points = Points.Values.ToList();

        for (int i = 0; i < points.Count; i++)
        {
            var point2 = points[i];

            if (Distance(point2, point) < NodeSize.Width*2 + 1)
            {
                return true;
            }
        }

        return false;
    }

    protected double Distance(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point1.X - point2.X,2) + Math.Pow(point1.Y - point2.Y, 2));
    }
}
