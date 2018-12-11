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

        /// <summary>
        /// статистика игры
        /// </summary>
        public static Log gameLog = new Log();

        /// <summary>
        /// уровень
        /// </summary>
        public static int gameLevel = 1;
        /// <summary>
        /// пауза между уровнями
        /// </summary>
        public static int gamePause = 40;  
        /// <summary>
        /// зазор между пулями
        /// </summary>
        public static int bulletPause = 1;   //зазор между пулями

        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        

        static Game()
        {
            logStr += gameLog.WriteLog;
        }
                
        public static List<BaseObject> _objs;   //вместо массива, что в примере, используем список
        private static List<Bullet> _bullets;
                
        private static List<Asteroid> _asteroids;
        private static FirstAid[] _firstAid;

        static Size bulletSize = new Size(4, 1);

        /// <summary>
        /// добавляет новую пулю в коллекцию
        /// </summary>
        public static void NewBullet()
        {
            if (!(Bullet.Pause > 0))
            {
                _bullets.Add(new Bullet(
                new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));

                Bullet.Pause = bulletPause;   //зазор между пулями
            }
        }

        /// <summary>
        /// Заполняет коллекцию астероидами
        /// </summary>
        /// <param name="nAst">количество астероидов</param>
        public static void MakeAsteroids(int nAst)
        {
            if (!(Asteroid.Pause > 0))
            {                
                for (var i = 0; i < nAst; i++)
                {                    
                    int power = Asteroid.NewPower();
                    int r = Asteroid.NewR(power);
                       
                    _asteroids.Add(new Asteroid(power, new Point(Game.Width, Rnd.Next(0, Game.Height)),
                        Asteroid.NewDir(r), Asteroid.NewSize(r)));

                    _asteroids[i].logStr += gameLog.UpScore;
                    _asteroids[i].logStr += gameLog.WriteLog;
                }
            }
        }

        /// <summary>
        /// Заполняем коллекцию аптечками
        /// </summary>
        public static void NewFirstAid()
        {
            int indent = 100;   //отступ между аптечками
            int margin = 50;    //отстут от верхнего и нижнего краев экрана

            Image newImage = Image.FromFile("firstaid.png");
            for (var i = 0; i < _firstAid.Length; i++)
            {
                _firstAid[i] = new FirstAid(newImage, new Point(Game.Width + i * indent,
                    Rnd.Next(margin, Game.Height - margin)), new Point(-10, Rnd.Next(0, 20) - 10),
                    new Size(newImage.Width, newImage.Height));
            }
        }
        /// <summary>
        /// загружаем стартовые объекты
        /// </summary>
        public static void Load()
        {
            logStr(null, "загрузка объектов");

            _objs = new List<BaseObject>();
            //_bullet = new Bullet(new Point(0, 200), new Point(5, 0), bulletSize);
            _bullets = new List<Bullet>();
            _asteroids = new List<Asteroid>();

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
            Asteroid.Pause = gamePause;    //Астероиды появятся с началом уровня

            //аптечки
            NewFirstAid();

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
        
        /// <summary>
        /// инициализация графического поля
        /// </summary>
        /// <param name="form"></param>
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

            Timer timer = new Timer { Interval = 1000 };
            _timer.Start();
            _timer.Tick += Timer_Tick;

            Ship.MessageDie += Finish;           

        }

        /// <summary>
        /// завершение игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 
                60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            logStr(null, "игра окончена");
        }
        /// <summary>
        /// действия по тику таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }        
        /// <summary>
        /// Клавиши управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                NewBullet();
            }
            else
            {
                if (e.KeyCode == Keys.Up) _ship.Up();
                if (e.KeyCode == Keys.Down) _ship.Down();

                if (e.Modifiers == Keys.Control)
                {
                    NewBullet();
                }
            }
        }
        
        /// <summary>
        /// Отрисовка игшрового процесса
        /// </summary>
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
                    + " Bullets:" + _bullets.Count
                    + " Hit:" + gameLog.Hit
                    + " Destroyed:" + gameLog.Destroyed
                    + " Asteroids:" + _asteroids.Count
                    + " Level:" + gameLevel
                    , SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }
            if (Asteroid.Pause > 0)
            {
                Buffer.Graphics.DrawString("Level " + gameLevel, new Font(FontFamily.GenericSansSerif,
                60, FontStyle.Underline), Brushes.White, 200, 100);
            }

            Buffer.Render();
        }
        /// <summary>
        /// Начинает новый уровень
        /// </summary>
        public static void NewLevel()
        {
            if (!(Asteroid.Pause > 0))            {              

                MakeAsteroids(2 + gameLevel);

                _bullets.Clear();
            }
            else
            {
                
            }            
        }
        /// <summary>
        /// Изменение состояния объектов
        /// </summary>
        public static void Update()
        {      

            if (_asteroids.Count == 0)
            {
                NewLevel();
            }

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
                        
            int a = 0;
            bool destr = false;
            while (a < _asteroids.Count)
            {                
                if (_asteroids[a] == null) continue;

                destr = false;
                _asteroids[a].Update();                

                b = 0;
                while (b < _bullets.Count && destr == false)                {
                    
                    if (_bullets[b] != null && _bullets[b].Collision(_asteroids[a]))                    
                    {
                        System.Media.SystemSounds.Hand.Play();
                                            
                        destr = _asteroids[a].Hit(_bullets[b]);
                        
                        RemoveBullet(b);                        
                    }
                    else { b++; }
                }

                if (destr) { RemoveAst(a); }
                else
                {
                    if (_ship.Collision(_asteroids[a]))
                    {
                        var rnd = new Random();
                        _ship?.EnergyLow(_asteroids[a], _asteroids[a].Power);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (_ship.Energy <= 0) _ship?.Die();
                    }

                    a++;
                }
                
            }            

            if (_asteroids.Count == 0)
            {
                if (!(Asteroid.Pause > 0))
                {
                    gameLevel++;
                    Asteroid.Pause = gamePause;
                }                
            }

            Bullet.LowPause();
            Asteroid.LowPause();

        }        
        /// <summary>
        /// Удалить пулю из игры
        /// </summary>
        /// <param name="b">номер элемента в списке, содержащий пулю</param>
        public static void RemoveBullet(int b)
        {
            _bullets[b] = null;
            _bullets.RemoveAt(b);
        }
        /// <summary>
        /// Удалить астероид из игры
        /// </summary>
        /// <param name="a">номер элемента в списке, содержащий астероид</param>
        public static void RemoveAst(int a)
        {
            _asteroids[a] = null;
            _asteroids.RemoveAt(a);
        }
    }
}