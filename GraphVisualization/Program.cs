using GraphVisualization.FileOperating;
using GraphVisualization.GraphDrawers;
using SkiaSharp;
using System.Diagnostics;
using System.Drawing;

int W = 512*2;
int H = 512*2;

Rectangle randomZone = new(20,20, W - 60, H - 60);

SKBitmap bitmap = new SKBitmap(W, H);

using PictureGenerator gen = new TreePictureGenerator(
    bitmap,
    GraphReader.ReadGraph("tree2.txt", true, false));



gen.Draw();

gen.Save("myfile.jpg");

Console.WriteLine("Всё сгенерированно");

var p = Process.Start("explorer.exe", "myfile.jpg");