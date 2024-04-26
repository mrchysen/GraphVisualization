using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitmapPractise.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BitmapPractise.Graph
{
    public abstract class GraphPictureGenerator : PictureGenerator
    {
        protected Graph graph;
        public Size NodeSize { get; set; } = new(20, 20);
        public Pen DefaultPen { get; set; } = null!;
        public Brush DefaultBrush { get; set; } = new SolidBrush(Color.Black);

        public GraphPictureGenerator(Bitmap picture, Graphics graphics, Graph graph) : base(picture, graphics)
        {
            this.graph = graph;
            DefaultPen = new Pen(DefaultBrush, 2);
        }

        public override void Draw()
        {
            DrawGraph();
        }

        protected abstract void DrawGraph();

        protected void DrawEdge(Point begin, Point end)
        {
            DrawArrow(begin, end);

            graphics.DrawLine(DefaultPen, new Point(begin.X + NodeSize.Width / 2, begin.Y + +NodeSize.Height / 2), new Point(end.X + NodeSize.Width / 2, end.Y + +NodeSize.Height / 2));
        }

        protected void DrawArrow(Point begin, Point end)
        {
            double dlina = Module(new Point(-end.X - begin.X, -end.Y - begin.Y));
            double lambda = NodeSize.Width / dlina;

            Console.WriteLine(begin);
            Console.WriteLine(end);
            Console.WriteLine(dlina);
            Console.WriteLine(lambda);

            Point point = new Point(-end.X - begin.X, -end.Y - begin.Y);
            point = new Point((int)(point.X * lambda) + end.X, (int)(point.Y * lambda) + end.Y);

            var point1 = new Point(point.X,point.Y + 100);

            Console.WriteLine(point);
            Console.WriteLine(point1);

            graphics.DrawLine(DefaultPen, point, point1);
        }

        protected double Module(Point point) => Math.Sqrt(point.X * point.X + point.Y * point.Y);

        protected void DrawNode(int Num, Point point)
        {
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(point, NodeSize));
            graphics.DrawEllipse(DefaultPen, new Rectangle(point, NodeSize));

            var NumberPoint = new Point(point.X + NodeSize.Width / 2 - Num.Digits() * 6 + Num.Digits(), point.Y + NodeSize.Height / 2 - 8);

            graphics.DrawString(Num.ToString(), new Font("Arial", 11), DefaultBrush, NumberPoint);
        }
    }
}
