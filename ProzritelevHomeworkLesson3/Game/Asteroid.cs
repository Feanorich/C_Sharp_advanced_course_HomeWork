using System;
using System.Drawing;
namespace MyGame
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        public int Power { get; set; }
        
        public Asteroid(int _power, Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = _power;
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
            Game.Buffer.Graphics.FillEllipse(Brushes.Khaki, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }

        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
        }

        //перенесли реализацию метода из BaseObject ?       
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
            Asteroid asteroid = new Asteroid(Power, new Point(Pos.X, Pos.Y), 
                new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;
        }

        /// <summary>
        /// Астероиду нанесен урон
        /// </summary>
        /// <param name="bo">объект нанесший урон</param>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public void Hit(BaseObject bo)
        {
            Power--;
            if (Power <= 0)
            {
                ToLog(this, "астероид уничтожен");

                Random rnd = new Random();
                int _power = rnd.Next(1, 5);
                int r = _power * 10;
                this.Restart(new Point(Game.Width, rnd.Next(0, Game.Height)),
                            new Point(-r / 5, r), new Size(r, r));
                Power = _power;
            }
            else
            {
                ToLog(bo, "астероид поврежден");
                int r = Power * 10;
                int nX = Dir.X * 5 / Size.Width;
                int nY = Dir.Y / Size.Height;
                Dir.X = r / 5 * nX;
                Dir.Y = r * nY;
                Size.Width = r;
                Size.Height = r;
            }
        }
        
    }
}