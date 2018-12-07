using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{
    static class Game
    {
        public static event LogStr logStr;        

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer _timer = new Timer();
        public static Random Rnd = new Random();

        public static Log gameLog = new Log();

        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        

        static Game()
        {
            logStr += gameLog.WriteLog;
        }
                
        public static List<BaseObject> _objs;   //вместо массива, что в примере, используем список
        private static List<Bullet> _bullets;
                
        private static Asteroid[] _asteroids;
        private static FirstAid[] _firstAid;

        static Size bulletSize = new Size(4, 1);

        public static void Load()
        {
            logStr(null, "загрузка объектов");

            _objs = new List<BaseObject>();
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), bulletSize);
            _bullets = new List<Bullet>();

            _asteroids = new Asteroid[15];
            _firstAid = new FirstAid[4];

            Random rnd = new Random();

            //звезды
            int nStars = 30;
            for (int i = 0; i < nStars; i++)
            {
                int r = rnd.Next(5, 50);
                _objs.Add(new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3)));
            }
            
            //астероиды
            for (var i = 0; i < _asteroids.Length; i++) 
            {
                int power = rnd.Next(1, 5);
                int r = power * 10;
                _asteroids[i] = new Asteroid(power, new Point(Game.Width, rnd.Next(0, Game.Height)), 
                    new Point(-r / 5, r), new Size(r, r));

                _asteroids[i].logStr += gameLog.UpScore;
                _asteroids[i].logStr += gameLog.WriteLog;
            }

            //аптечки
            Image newImage = Image.FromFile("firstaid.png");            
            for (var i = 0; i < _firstAid.Length; i++)
            {                
                _firstAid[i] = new FirstAid(newImage, new Point(Game.Width+i * newImage.Width, 
                    rnd.Next(100, Game.Height-100)), new Point( -10, rnd.Next(0, 20) - 10), 
                    new Size(newImage.Width, newImage.Height));                
            }            

            _ship.logStr += gameLog.WriteLog;

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
        

        public static void Init(Form form)
        {
            logStr(null, "инициализация");

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

            form.KeyDown += Form_KeyDown;

            //Timer timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;

            Ship.MessageDie += Finish;           

        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 
                60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            logStr(null, "игра окончена");
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add(new Bullet(
                new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));

            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        public static void DrawObj(BaseObject[] _arr)
        {            
            foreach (BaseObject _item in _arr)
            { _item?.Draw(); }
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in _objs)
            { obj?.Draw(); }

            foreach (Asteroid a in _asteroids)
            { a?.Draw(); }

            foreach (BaseObject _bullet in _bullets)
            { _bullet?.Draw(); }

            foreach (BaseObject fa in _firstAid)
            { fa?.Draw(); }

            _ship?.Draw();

            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy 
                    +" Bullets:" + _bullets.Count
                    + " Hit:" + gameLog.Hit
                    +" Destroyed:" + gameLog.Destroyed,
                    SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj.Update();                
            }

            int b = 0;
            while (b < _bullets.Count)
            {
                _bullets[b].Update();
                if (_bullets[b].Out())
                {                    
                    RemoveBullet(b);
                }
                else { b++; }
            }            

            foreach (BaseObject _bullet in _bullets)
            { _bullet?.Update(); }

            for (var i = 0; i < _firstAid.Length; i++)
            {
                if (_firstAid[i] == null) continue;
                _firstAid[i].Update();

                if (!_ship.Collision(_firstAid[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(_firstAid[i], _firstAid[i].Energy);

                System.Media.SystemSounds.Beep.Play();

                _firstAid[i].Restart(new Point(Game.Width, rnd.Next(100, Game.Height - 100)),
                    new Point(-10, rnd.Next(0, 20) - 10));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();

                b = 0;
                while (b < _bullets.Count)
                {
                    if (_bullets[b] != null && _bullets[b].Collision(_asteroids[i]))                    
                        {
                        System.Media.SystemSounds.Hand.Play();

                        //воскресим астероид в начале экрана
                        int power = Rnd.Next(1, 5);
                        int r = power * 10;
                        _asteroids[i].Hit(_bullets[b]);

                        //_bullets[b] = null;
                        //_bullets.RemoveAt(b);
                        RemoveBullet(b);
                        continue;
                    }
                    else { b++; }
                }

                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(_asteroids[i], _asteroids[i].Power);
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship?.Die();
            }
        }

        /* Старый апдейт
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
        */
        /// <summary>
        /// Удалить пулю из игры
        /// </summary>
        /// <param name="b">номер элемента в списке, содержащий пулю</param>
        public static void RemoveBullet(int b)
        {
            _bullets[b] = null;
            _bullets.RemoveAt(b);
        }
    }
}