using BitmapPractise;
using System.Diagnostics;
using System.Drawing;

int W = 800;
int H = 800;

Bitmap bitmap = new Bitmap(W, H);

using (PictureGenerator gen = new GraphPictureGenerator(bitmap, Graphics.FromImage(bitmap), GraphReader.ReadGraph("graph1.txt", true)))
{
    gen.Draw();

    gen.Save("myfile.jpg");
}

Console.WriteLine("Всё сгенерированно");

var p = Process.Start("explorer.exe", "myfile.jpg");