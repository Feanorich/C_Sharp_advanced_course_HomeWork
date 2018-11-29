using System;
using System.Drawing;
namespace MyGame
{
    class StarImg : BaseObject
    {
        protected Image img;

        public StarImg(Image _img, Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            img = _img;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(img, Pos.X, Pos.Y, Size.Width, Size.Height);            
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X <= 0) { Dir.X = -Dir.X; Pos.X = 0; }
            if (Pos.X >= (Game.Width - Size.Width)) { Dir.X = -Dir.X; Pos.X = (Game.Width - Size.Width); }
            if (Pos.Y <= 0) { Dir.Y = -Dir.Y; Pos.Y = 0; }
            if (Pos.Y >= (Game.Height - Size.Height)) { Dir.Y = -Dir.Y; Pos.Y = (Game.Height - Size.Height); }
        }
    }
}