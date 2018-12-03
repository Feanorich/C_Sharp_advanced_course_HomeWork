using System;
using System.Drawing;
namespace MyGame
{
    class Asteroid : BaseObject, ICloneable 
    {
        public int Power { get; set; }
        
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        protected override void CheckObject()
        {
            int maxSize = 50;  //ограничим максимальный размер астероида
            if (Size.Width > maxSize || Size.Height > maxSize)
            {
                throw new GameObjectException(this.GetType() +
                    " должен иметь размер не превышающий " + 
                    maxSize + " текущий размер "+Size.Width+","+Size.Height);
            }            
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Khaki , Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }

        //перенесли реализацию метода из BaseObject        
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public object Clone()
        {
            // Создаем копию нашего робота
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), 
                new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;
        }       
        
    }
}