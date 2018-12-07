using System;
using System.Drawing;
namespace MyGame
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        protected override void CheckObject()
        {
            int maxSpeed = 20;  //ограничим максимальную скорость пули
            if ((Pos.X <= 0 && Dir.X <= 0) || (Pos.X >= Game.Width && Dir.X >= 0))
            {
                throw new GameObjectException(this.GetType() + 
                    " летит в направлении, что не позволит ей появиться на экране");
            } else if (Dir.X > maxSpeed)
            {
                throw new GameObjectException(this.GetType() +
                    " не должна иметь стартовую скорость выше " + 
                    maxSpeed + " текущая скорость " + Dir.X);
            }
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width,
            Size.Height);
        }        

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;            
        }   
        

        public bool Out()
        {            
            return (Pos.X > Game.Width) ? true: false;
        }
    }
}