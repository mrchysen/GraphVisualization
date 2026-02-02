using SkiaSharp;
using System.Drawing;

namespace GraphVisualization.GraphRenderer;

public class RendererOptions
{
    public SKPaint Theme { get; set; } = new()
    {
        Color = new SKColor(50, 50, 50),
        Style = SKPaintStyle.Fill
    };

    public int BottomMargin { get; set; } = 10;

    public SKFont Font { get; set; } = new()
    {   
        Size = 32.0f,
    };

    public SKPaint FontPaint { get; set; } = new()
    {
        IsAntialias = true,
        Color = new SKColor(0, 0, 0),
        Style = SKPaintStyle.Fill
    };

    public SKPaint ColorPaint { get; set; } = new()
    {
        Color = new SKColor(0,255,255)
    };

    public SKPaint LinePaint { get; set; } = new()
    {
        Color = new SKColor(120, 219, 226),
        StrokeWidth = 4,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
    };

    public SKPaint BorderNodePaint { get; set; } = new()
    {
        Color = new SKColor(181, 184, 177),
        Style = SKPaintStyle.Stroke,
        IsAntialias = true,
        StrokeWidth = 2
    };
    public SKPaint NodePaint { get; set; } = new()
    {
        Color = new SKColor(120, 219, 226),
        Style = SKPaintStyle.Fill,
    };

    public float ArrowAngle { get; set; } = MathF.PI / 6;

    public float ArrowSize { get; set; } = 1f;

    public SKPaint ArrowPaint { get; set; } = new()
    {
        Color = new SKColor(120, 219, 226),
        Style = SKPaintStyle.StrokeAndFill,
    };
    public SKPaint BorderArrowPaint { get; set; } = new()
    {
        Color = new SKColor(120, 219, 226),
        Style = SKPaintStyle.Stroke,
        IsAntialias = true,
        StrokeWidth = 2
    };

    public SKPaint DebugPaint { get; set; } = new()
    {
        Color = new SKColor(255, 0, 0),
        StrokeWidth = 4,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
    };

    public Size NodeSize { get; set; } = new(20, 20);

    public static RendererOptions CreateDefault() 
        => new();
}