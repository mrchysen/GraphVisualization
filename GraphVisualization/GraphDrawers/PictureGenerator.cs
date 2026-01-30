using SkiaSharp;

namespace GraphVisualization.GraphDrawers;

public abstract class PictureGenerator : IDisposable
{
    protected SKBitmap _bitmap;
    protected SKCanvas _canvas;

    public PictureGenerator(SKBitmap bitmap)
    {
        _bitmap = bitmap;
        _canvas = new SKCanvas(bitmap);

        _canvas.Clear(SKColors.White);
    }

    public abstract void Draw();
    
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
}
