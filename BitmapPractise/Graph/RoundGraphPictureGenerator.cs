using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPractise.Graph;

public class RoundGraphPictureGenerator : GraphPictureGenerator
{
    public int R { get; set; }

    public RoundGraphPictureGenerator(Bitmap picture, Graphics graphics, Graph graph, int R = 100) : base(picture, graphics, graph)
    {
        this.R = R;
    }

    protected override void DrawGraph()
    {
        var n = graph.Count;

        var centre = new Point(picture.Size.Width / 2, picture.Size.Height / 2);
        double fi = 2 * Math.PI / n;

        for (int i = 0; i < n; i++)
        {
            var point = GetPointByNum(i, fi, centre);

            for (int j = 0; j < graph[i].Count; j++)
            {
                var point2 = GetPointByNum(graph[i][j], fi, centre);
                    
                DrawEdge(point, point2);
            }
        }

        for (int i = 0; i < n; i++)
        {
            var point = GetPointByNum(i, fi, centre);

            DrawNode(i + 1, point);
        }
    }

    protected Point GetPointByNum(int num, double fi, Point centre) => new Point(centre.X + (int)(R * Math.Cos(fi * num)), centre.Y + (int)(R * Math.Sin(fi * num)));

}
