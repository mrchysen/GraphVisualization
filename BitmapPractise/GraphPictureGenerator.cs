using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BitmapPractise
{
    public class GraphPictureGenerator : PictureGenerator
    {
        protected Graph graph;
        public Size NodeSize { get; set; } = new(20, 20);
        public Brush DefaultBrush { get; set; } = new SolidBrush(Color.Black);
        public Pen DefaultPen { get; set; } = null!;
        public int R { get; set; } = 100;
        public GraphPictureGenerator(Bitmap picture, Graphics graphics, Graph graph) : base(picture, graphics) 
        { 
            this.graph = graph;
            DefaultPen = new Pen(DefaultBrush);
        }

        // Тактика: 
        // 1. Нарисовать сначала рёбра
        // 2. Потом нарисовать вершины
        public override void Draw()
        {
            DrawGraph();
        }

        protected virtual void DrawGraph()
        {
            var n = graph.Count;

            var centre = new Point(picture.Size.Width / 2, picture.Size.Height / 2);
            double fi = 2 * Math.PI / n;

            for (int i = 0; i < n; i++)
            {
                var point = GetPointByNum(i, fi, centre);

                for (int j = 0; j < graph[i].Count; j++)
                {
                    if(i < graph[i][j])
                    {
                        var point2 = GetPointByNum(graph[i][j], fi, centre);
                        DrawEdge(point, point2);
                    }
                    
                }

                DrawNode(i + 1, point);
            }
        }

        protected Point GetPointByNum(int num, double fi, Point centre) => new Point(centre.X + (int)(R * Math.Cos(fi * num)), centre.Y + (int)(R * Math.Sin(fi * num)));

        protected void DrawEdge(Point begin, Point end)
        {
            graphics.DrawLine(DefaultPen, new Point(begin.X + NodeSize.Width / 2, begin.Y + +NodeSize.Height / 2), new Point(end.X + NodeSize.Width / 2, end.Y + +NodeSize.Height / 2));
        }

        protected void DrawNode(int Num, Point point) 
        {
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(point, NodeSize));
            graphics.DrawEllipse(DefaultPen, new Rectangle(point, NodeSize));
            
            var NumberPoint = new Point(point.X + NodeSize.Width / 2 - (Num.Digits()*12) /2 + (Num.Digits()*2)/2, point.Y + NodeSize.Height / 2 - 8);
            
            graphics.DrawString(Num.ToString(), new Font("Arial", 11), DefaultBrush, NumberPoint);
        }
    }
}
