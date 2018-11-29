using System;
using System.Drawing;
namespace MyGame
{
    class StarN : BaseObject
    {
        protected SolidBrush color;
        protected int nPoints; // число вершин       
        private int xStart;
        private int yStart;

        public StarN(Color _color, int _n, Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            color = new SolidBrush(_color);
            if (nPoints < 4) nPoints = 4;
            else nPoints = _n;
            xStart = Pos.X;
            yStart = Pos.Y;
        }

        public override void Draw()
        {
            //int sX = Size.Width / 4;
            //int sY = Size.Height / 4;
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X,
            //Pos.Y + Size.Height);
                          
            double R = Size.Height , r = Size.Width;   // радиусы
            //double r = Size.Height, R = Size.Width;   // радиусы
            double alpha = Math.PI + Math.PI / 2;         // поворот
            int x0 = Pos.X;
            int y0 = Pos.Y;

            PointF[] points = new PointF[2 * nPoints + 1];
            double a = alpha, da = Math.PI / nPoints, l;
            for (int k = 0; k < 2 * nPoints + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                a += da;
            }

            //Game.Buffer.Graphics.DrawLines(Pens.Yellow, points);            
            Game.Buffer.Graphics.FillPolygon(color , points);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0 || Pos.X > Game.Width || Pos.Y < 0 || Pos.Y > Game.Height)
            {
                Pos.X = xStart;
                Pos.Y = yStart;
            }
        }
    }
}