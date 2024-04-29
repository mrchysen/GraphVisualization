using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BitmapPractise.Extensions;

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
            DefaultPen = new Pen(DefaultBrush, 1);
        }

        public override void Draw()
        {
            DrawGraph();
        }

        protected abstract void DrawGraph();

        protected void DrawEdge(Point begin, Point end, Edge? edge = null)
        {
            DrawArrow(begin, end);

            graphics.DrawLine(DefaultPen, new Point(begin.X + NodeSize.Width / 2, begin.Y + NodeSize.Height / 2), new Point(end.X + NodeSize.Width / 2, end.Y +NodeSize.Height / 2));

            if (graph.IsWeighted)
            {
                if(edge == null)
                {
                    //throw new Exception("No set node for make weight visualization.");
                }

                DrawWeight(edge.Weight, begin, end);
            }
        }

        protected void DrawWeight(int weight, Point begin, Point end)
        {
            begin = new Point(begin.X + NodeSize.Width / 2, begin.Y + NodeSize.Height / 2);
            end = new Point(end.X + NodeSize.Width / 2, end.Y + NodeSize.Height / 2);

            Point centre = new Point((begin.X + end.X) / 2, (begin.Y + end.Y) / 2);

            Point textPoint = new Point(centre.X - 9 * weight.Digits(), centre.Y - 8);

            graphics.FillRectangle(new SolidBrush(Color.White),new Rectangle(textPoint, new Size(20 * weight.Digits(), 16)));
            graphics.DrawString(weight.ToString(), new Font("Arial", 11), DefaultBrush, textPoint);
        }

        protected void DrawArrow(Point begin, Point end)
        {
            begin = new Point(begin.X + NodeSize.Width / 2, begin.Y +NodeSize.Width / 2);
            end = new Point(end.X + NodeSize.Width / 2, end.Y + NodeSize.Width / 2);
            double phi = Math.PI / 6;

            Point subPoint = new Point(-begin.X + end.X,-begin.Y + end.Y);

            double r = NodeSize.Width / 2;
            double dlina = Norm(subPoint);

            Point vec = new Point((int)(subPoint.X * (r / dlina)), (int)(subPoint.Y * (r / dlina)));

            var vec1 = Rotate(phi, vec);
            var vec2 = Rotate(-phi, vec);

            Point point = new Point((int)(begin.X + subPoint.X * (r / dlina)), (int)(begin.Y + subPoint.Y * (r / dlina)));

            Point ArrowPoint1 = new Point(point.X + vec1.X, point.Y + vec1.Y);
            Point ArrowPoint2 = new Point(point.X + vec2.X, point.Y + vec2.Y);
            graphics.FillPolygon(DefaultBrush, new Point[] { ArrowPoint1, ArrowPoint2, point });
        }

        protected Point Rotate(double fi, Point p) => new Point((int)(p.X * Math.Cos(fi) - p.Y*Math.Sin(fi)),
                                                        (int)(p.X * Math.Sin(fi) + p.Y * Math.Cos(fi)));

        protected double Norm(Point point) => Math.Sqrt(point.X * point.X + point.Y * point.Y);

        protected void DrawNode(int Num, Point point)
        {
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(point, NodeSize));
            graphics.DrawEllipse(DefaultPen, new Rectangle(point, NodeSize));

            var NumberPoint = new Point(point.X + NodeSize.Width / 2 - Num.Digits() * 6 + Num.Digits(), point.Y + NodeSize.Height / 2 - 8); ;

            graphics.DrawString(Num.ToString(), new Font("Arial", 11), DefaultBrush, NumberPoint);
        }
    }
}
