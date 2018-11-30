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

        /// <summary>
        /// N-лучевая звезда
        /// </summary>
        /// <param name="_color">Цвет звезды</param>
        /// <param name="_n">Количество лучей</param>   
        public StarN(Color _color, int _n, Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            color = new SolidBrush(_color);
            if (_n < 4) { nPoints = 4; }
            else { nPoints = _n; }
            xStart = Pos.X;
            yStart = Pos.Y;
        }

        public override void Draw()
        {           
                          
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