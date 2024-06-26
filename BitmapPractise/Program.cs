﻿using BitmapPractise;
using BitmapPractise.Graph;
using System.Diagnostics;
using System.Drawing;

int W = 512;
int H = 512;

Rectangle rec = new(20,20, W - 60, H - 60);

Bitmap bitmap = new Bitmap(W, H);


using (PictureGenerator gen = new RandomGraphPictureGenerator(bitmap, Graphics.FromImage(bitmap), GraphReader.ReadGraph("graph2.txt", false, true), rec))
{
        gen.Draw();

        gen.Save("myfile.jpg");

}

Console.WriteLine("Всё сгенерированно");

var p = Process.Start("explorer.exe", "myfile.jpg");