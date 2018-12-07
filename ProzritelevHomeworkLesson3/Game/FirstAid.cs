using System;
using System.Drawing;
namespace MyGame
{
    class FirstAid : StarImg
    {
        private int _energy = -5;
        public int Energy => _energy;

        /// <summary>
        /// Картинка
        /// </summary>
        /// <param name="_img">Объект Image содержащий картинку</param>        
        public FirstAid(Image _img, Point pos, Point dir, Size size) : base(_img, pos, dir, size)
        {
            
        }        

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X <= -Size.Width)
            {
                Pos.X = Game.Width;
            }
            //if (Pos.X >= (Game.Width - Size.Width)) { Dir.X = -Dir.X; Pos.X = (Game.Width - Size.Width); }
            if (Pos.Y <= 0) { Dir.Y = -Dir.Y; Pos.Y = 0; }
            if (Pos.Y >= (Game.Height - Size.Height)) { Dir.Y = -Dir.Y; Pos.Y = (Game.Height - Size.Height); }
        }
    }
}