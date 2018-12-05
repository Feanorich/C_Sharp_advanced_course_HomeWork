using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        #region массив из BaseObject        
        public static List<BaseObject> _objs;   //вместо массива, что в примере, используем список

        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        static Size bulletSize = new Size(4, 1);

        public static void Load()
        {
            _objs = new List<BaseObject>();
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), bulletSize);
            _asteroids = new Asteroid[5];
            Random rnd = new Random();

            //звезды
            for (int i = 0; i < 30; i++)    
            {
                int r = rnd.Next(5, 50);
                _objs.Add(new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3)));
            }
            
            //астероиды
            for (var i = 0; i < _asteroids.Length; i++) 
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), 
                    new Point(-r / 5, r), new Size(r, r));
            }

            #region свои зездочки пока уберем
            ////добавим летающих картинок
            //Image newImage = Image.FromFile("HSGMs.png");
            //for (int i = 1; i < 16; i++)
            //    _objs.Add(new StarImg(newImage, new Point(300, i * 30), 
            //        new Point(i, i), new Size(newImage.Width, newImage.Height)));

            ////добавим желтых, семилучевых звездочек
            //for (int i = -2; i <= 2 ; i++)
            //{
            //    for (int j = -2; j <= 2 ; j++)
            //    {
            //        if (!(i == 0 && j == 0))
            //            _objs.Add(new StarN(Color.Yellow, 7, new Point(400 + i * 14, 300 + j * 14), 
            //                new Point(4 * i + 2*(-j+i), 4 * j + 2*(i+j)), new Size(7, 3)));
            //    }
            //}
            #endregion 
        }
        #endregion массив из BaseObject

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для 
            // текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }

            foreach (Asteroid obj in _asteroids)
            {
                obj.Draw();
            }

            _bullet.Draw();

            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();

                    Random rnd = new Random();

                    //не просто перенесем астероид в начало, а перерисуем его
                    //с новыми размерами и скоростью
                    int r = rnd.Next(5, 50);
                    a.Restart(new Point(Game.Width, rnd.Next(0, Game.Height)), 
                        new Point(-r / 5, r), new Size(r, r));

                    int margin = 100;   //отступ от края экрана
                    int speed = rnd.Next(5, 10);
                    _bullet.Restart(new Point(0, rnd.Next(margin, Game.Height - margin)), new Point(speed, 0));                   
                    
                }
            }
            _bullet.Update();
        }
    }
}