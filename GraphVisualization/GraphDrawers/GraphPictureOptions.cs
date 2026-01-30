using SkiaSharp;
using System.Drawing;

namespace GraphVisualization.GraphDrawers;

public class GraphPictureOptions
{
    public SKFont Font { get; set; } = new SKFont
    {   
        Size = 32.0f,
    };
    public SKPaint FontPaint { get; set; } = new SKPaint
    {
        IsAntialias = true,
        Color = new SKColor(0, 0, 0), // red
        Style = SKPaintStyle.Fill
    };

    public SKPaint ColorPaint { get; set; } = new SKPaint()
    {
        Color = new SKColor(0,255,255)
    };

    public SKPaint LinePaint { get; set; } = new SKPaint()
    {
        Color = new SKColor(255, 0, 255),
        StrokeWidth = 4,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
    };

    public SKPaint NodePaint { get; set; } = new SKPaint()
    {
        Color = new SKColor(100, 100, 100),
        StrokeWidth = 4,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
    };

    public SKPaint DebugPaint { get; set; } = new SKPaint()
    {
        Color = new SKColor(255, 0, 0),
        StrokeWidth = 4,
        IsAntialias = true,
        Style = SKPaintStyle.Stroke
    };

    public Size NodeSize { get; set; } = new(20, 20);

    public static GraphPictureOptions CreateDefault()
    {
        return new GraphPictureOptions();
    }
}