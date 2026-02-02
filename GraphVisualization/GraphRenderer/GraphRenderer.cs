using GraphVisualization.Models;
using SkiaSharp;

namespace GraphVisualization.GraphRenderer;

public abstract class GraphRenderer : IDisposable
{
    protected SKBitmap _bitmap;
    protected SKCanvas _canvas;

    protected Graph _graph;

    public RendererOptions Options { get; private set; }

    public GraphRenderer(
        SKBitmap bitmap,
        Graph graph,
        RendererOptions? options = null)
    {
        _bitmap = bitmap;
        _canvas = new SKCanvas(bitmap);
        _graph = graph;

        Options = options ?? RendererOptions.CreateDefault();

        _canvas.Clear(Options.Theme.Color);
    }

    public abstract void Render();

    public void Save(string filename)
    {
        using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
        using var image = SKImage.FromBitmap(_bitmap);
        using var encodedImage = image.Encode();
        encodedImage.SaveTo(stream);
    }

    public void Dispose()
    {
        _canvas.Dispose();
        _bitmap.Dispose();
    }

    #region DrawMethods
    protected void DrawEdge(
        SKPoint begin, SKPoint end, Edge? edge = null)
    {
        DrawArrow(begin, end);

        _canvas.DrawLine(
            new SKPoint(begin.X, begin.Y),
            new SKPoint(end.X, end.Y),
            Options.LinePaint);

        if (edge is not null && _graph.IsWeighted)
        {
            DrawWeigtBetweenNodes(edge.Weight.ToString(), begin, end);
        }
    }

    protected void DrawWeigtBetweenNodes(
        string weight,
        SKPoint begin,
        SKPoint end)
    {
        var centre = new SKPoint((begin.X + end.X) / 2, (begin.Y + end.Y) / 2);

        float textWidth = Options.Font.MeasureText(weight, Options.FontPaint);
        float textHeight = Options.Font.Size;

        var numberPoint = new SKPoint(
            centre.X - (int)(textWidth / 2),
            centre.Y + (int)(textHeight / 3));

        _canvas.DrawRect(numberPoint.X, numberPoint.Y - textHeight, textWidth, textHeight + Options.BottomMargin, Options.Theme);
        _canvas.DrawText(weight, numberPoint.X, numberPoint.Y, Options.Font, Options.FontPaint);
    }

    /// <summary>
    /// Draw arrow from p1 to p2
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    protected void DrawArrow(SKPoint p1, SKPoint p2)
    {
        var p3 = p1 - p2;

        double r = Options.NodeSize.Width;
        double norm = Norm(p3);

        var p3unit = new SKPoint(
            (float)(p3.X * (r / norm)),
            (float)(p3.Y * (r / norm)));

        var subVec1 = Rotate(Options.ArrowAngle, p3unit);
        var subVec2 = Rotate(-Options.ArrowAngle, p3unit);

        var t = new SKPoint(
            (float)(p2.X + p3.X * (r / norm)),
            (float)(p2.Y + p3.Y * (r / norm)));

        var t1 = new SKPoint(
            t.X + subVec1.X * Options.ArrowSize,
            t.Y + subVec1.Y * Options.ArrowSize);

        var t2 = new SKPoint(
            t.X + subVec2.X * Options.ArrowSize,
            t.Y + subVec2.Y * Options.ArrowSize);

        var polygonPath = new SKPath();
        polygonPath.AddPoly([t1, t2, t], true);

        _canvas.DrawPath(polygonPath, Options.ArrowPaint);
        _canvas.DrawPath(polygonPath, Options.BorderArrowPaint);
    }

    protected void DrawNode(int num, SKPoint point)
    {
        _canvas.DrawPoint(
            point,
            Options.DebugPaint);

        _canvas.DrawOval(
            point,
            new SKSize(Options.NodeSize.Width, Options.NodeSize.Height),
            Options.NodePaint);
        _canvas.DrawOval(
            point,
            new SKSize(Options.NodeSize.Width, Options.NodeSize.Height),
            Options.BorderNodePaint);

        float textWidth = Options.Font.MeasureText(num.ToString(), Options.FontPaint);
        float textHeight = Options.Font.Size;

        var NumberPoint = new SKPoint(
            point.X - (int)(textWidth / 2),
            point.Y + (int)(textHeight / 3));

        _canvas.DrawText(num.ToString(), NumberPoint.X, NumberPoint.Y, Options.Font, Options.FontPaint);
    }

    private SKPoint Rotate(double fi, SKPoint p) =>
        new((float)(p.X * Math.Cos(fi) - p.Y * Math.Sin(fi)),
             (float)(p.X * Math.Sin(fi) + p.Y * Math.Cos(fi)));

    private float Norm(SKPoint point) =>
        MathF.Sqrt(point.X * point.X + point.Y * point.Y);

    #endregion
}
