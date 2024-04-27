using BitmapPractise;
using BitmapPractise.Graph;
using System.Diagnostics;
using System.Drawing;

int W = 256;
int H = 256;

Rectangle rec = new(20,20, W - 40, H - 40);

Bitmap bitmap = new Bitmap(W, H);

using (PictureGenerator gen = new RoundGraphPictureGenerator(bitmap, Graphics.FromImage(bitmap), GraphReader.ReadGraph("graph2.txt")))
{
    gen.Draw();

    gen.Save("myfile.jpg");
}

Console.WriteLine("Всё сгенерированно");

var p = Process.Start("explorer.exe", "myfile.jpg");

//while(p.)