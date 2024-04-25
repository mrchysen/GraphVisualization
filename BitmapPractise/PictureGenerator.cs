using System.Drawing;

namespace BitmapPractise;

public abstract class PictureGenerator : IDisposable
{
    protected Bitmap picture;
    protected Graphics graphics;

    public PictureGenerator(Bitmap picture, Graphics graphics)
    {
        this.picture = picture;
        this.graphics = graphics;

        graphics.FillRectangle(new SolidBrush(Color.White), new(0, 0, picture.Width, picture.Height));
    }

    public abstract void Draw();
    
    public void Save(string filename) 
    { 
        picture.Save(filename);
    }

    public void Dispose()
    {
        graphics.Dispose();
        picture.Dispose();
    }
}
