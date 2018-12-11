using System;
using System.Drawing;
namespace MyGame
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        static int pause;

        public static Random Rnd;
        public int Power { get; set; }
        
        static Asteroid()
        {
            Rnd = new Random();
            pause = 0;
        }

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
        /// <returns> true - астероид уничтожен. false - нет</returns>
        public bool Hit(BaseObject bo)
        {
            Power--;
            if (Power <= 0)
            {
                ToLog(this, "астероид уничтожен");               

                return true;
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

                return false;
            }
        }

        #region статические методы
        /// <summary>
        /// Создает значение power для нового астероида
        /// </summary>
        /// <returns></returns>
        public static int NewPower()
        {
            return Rnd.Next(1, 5);
        }
        /// <summary>
        /// Создает радиус для нового астероида
        /// </summary>
        /// <returns></returns>
        public static int NewR(int _power)
        {
            return _power * 10; 
        }
        /// <summary>
        /// Создает значение скорости нового астероида
        /// </summary>
        /// <param name="_r"></param>
        /// <returns></returns>
        public static Point NewDir(int _r)
        {

            return new Point(-Rnd.Next(_r / 5, _r / 2 ) , _r);            
        }
        /// <summary>
        /// Создает значение размера нового астероида
        /// </summary>
        /// <param name="_r"></param>
        /// <returns></returns>
        public static Size NewSize(int _r)
        {
            return new Size(_r, _r);
        }

        /// <summary>
        /// Значение счетчика паузы (в игровых тиках)
        /// </summary>
        public static int Pause
        {
            get { return pause; }
            set
            {
                if (value >= 0) { pause = value; }
                else { pause = 0; }
            }
        }
        /// <summary>
        /// уменьшает счетчик паущы на 1
        /// </summary>
        public static void LowPause()
        {
            if (pause > 0)
            {
                pause--;
            }
        }
        #endregion

    }
}