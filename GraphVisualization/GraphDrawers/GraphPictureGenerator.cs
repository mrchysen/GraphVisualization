using GraphVisualization.Extensions;
using GraphVisualization.Models;
using SkiaSharp;
using System.Drawing;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace GraphVisualization.GraphDrawers;

public abstract class GraphPictureGenerator : PictureGenerator
{
    protected Graph _graph;

    public GraphPictureOptions Options { get; private set; }

    public GraphPictureGenerator(
        SKBitmap bitmap, 
        Graph graph,
        GraphPictureOptions? options = null) : base(bitmap)
    {
        _graph = graph;

        Options = options ?? GraphPictureOptions.CreateDefault();
    }

    protected void DrawEdge(Point begin, Point end, Edge? edge = null)
    {
        //DrawArrow(begin, end);

        _canvas.DrawLine(
            new SKPoint(begin.X, begin.Y), 
            new SKPoint(end.X, end.Y),
            Options.LinePaint);

        if (_graph.IsWeighted)
        {
            //DrawWeight(edge.Weight, begin, end);
        }
    }

    protected void DrawWeight(int weight, Point begin, Point end)
    {
        begin = new Point(begin.X + Options.NodeSize.Width / 2, begin.Y + Options.NodeSize.Height / 2);
        end = new Point(end.X + Options.NodeSize.Width / 2, end.Y + Options.NodeSize.Height / 2);

        Point centre = new Point((begin.X + end.X) / 2, (begin.Y + end.Y) / 2);

        Point textPoint = new Point(centre.X - 9 * weight.Digits() + 4, centre.Y - 8);

        var size = new Size(11 * weight.Digits(), 17);

        //_canvas.DrawRect(new SKRect(textPoint.X, textPoint.Y, size.Width, size.Height), Options.ColorPaint);
        _canvas.DrawText(weight.ToString(), textPoint.X, textPoint.Y, Options.Font, Options.FontPaint);
    }

    protected void DrawArrow(Point begin, Point end)
    {
        begin = new Point(begin.X + Options.NodeSize.Width / 2, begin.Y + Options.NodeSize.Width / 2);
        end = new Point(end.X + Options.NodeSize.Width / 2, end.Y + Options.NodeSize.Width / 2);
        double phi = Math.PI / 6;

        Point subPoint = new Point(-begin.X + end.X,-begin.Y + end.Y);

        double r = Options.NodeSize.Width / 2;
        double dlina = Norm(subPoint);

        Point vec = new Point((int)(subPoint.X * (r / dlina)), (int)(subPoint.Y * (r / dlina)));

        var vec1 = Rotate(phi, vec);
        var vec2 = Rotate(-phi, vec);

        var point = new SKPoint((int)(begin.X + subPoint.X * (r / dlina)), (int)(begin.Y + subPoint.Y * (r / dlina)));

        var ArrowPoint1 = new SKPoint(point.X + vec1.X, point.Y + vec1.Y);
        var ArrowPoint2 = new SKPoint(point.X + vec2.X, point.Y + vec2.Y);
        
        _canvas.DrawPoints(SKPointMode.Polygon, [ArrowPoint1, ArrowPoint2, point], Options.ColorPaint);
    }

    protected Point Rotate(double fi, Point p) => new Point((int)(p.X * Math.Cos(fi) - p.Y*Math.Sin(fi)),
                                                    (int)(p.X * Math.Sin(fi) + p.Y * Math.Cos(fi)));

    protected double Norm(Point point) => Math.Sqrt(point.X * point.X + point.Y * point.Y);

    protected void DrawNode(int num, Point point)
    {
        _canvas.DrawPoint(
            new SKPoint(point.X, point.Y),
            Options.DebugPaint);

        _canvas.DrawOval(
            new SKPoint(point.X, point.Y),
            new SKSize(Options.NodeSize.Width, Options.NodeSize.Height),
            Options.NodePaint);

        float textWidth = Options.Font.MeasureText(num.ToString(), Options.FontPaint);
        float textHeight = Options.Font.Size;

        var NumberPoint = new Point(
            point.X - (int)(textWidth / 2), 
            point.Y + (int)(textHeight / 3));

        _canvas.DrawText(num.ToString(), NumberPoint.X, NumberPoint.Y, Options.Font, Options.FontPaint);
    }
}
