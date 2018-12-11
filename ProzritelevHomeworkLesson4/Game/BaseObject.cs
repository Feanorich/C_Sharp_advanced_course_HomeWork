using System;
using System.Drawing;
namespace MyGame
{
    public delegate void Message();
    public delegate void LogStr(object o, string msg);

    abstract class BaseObject : ICollision, IRestart
    {      

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public event LogStr logStr;        

        /// <summary>
        /// Конструктор базового объекта
        /// </summary>
        /// <param name="pos">Объект Point задающий координаты</param>
        /// <param name="dir">Объект Point задающий скорость перемещения</param>
        /// <param name="size">Объект Size задающий размер</param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;

            CheckBaseObject();
            CheckObject();
        }

        /// <summary>
        /// Обязательная проберка объекта
        /// </summary>
        protected void CheckBaseObject()
        {
            if (Size.Width < 0 || Size.Height < 0)
            {
                throw new GameObjectException(this.GetType()+" не может быть отрицательного размера");
            }
        }
        /// <summary>
        /// Добровольная проверка объекта
        /// </summary>
        protected virtual void CheckObject()
        {
            //реализуется, если нужно, в наследниках
        }

        public abstract void Draw();
        
        public abstract void Update();

        //столкновения
        /// <summary>
        /// Проверка столкновения объектов
        /// </summary>
        /// <param name="o">объект с кем проверяем столкновение</param>
        /// <returns>true - было столкновение</returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

        //перезапуск объекта
        public void Restart(Point pos)
        {
            Pos.X = pos.X;
            Pos.Y = pos.Y;
        }

        public void Restart(Point pos, Point dir)
        {
            Restart(pos);
            Dir.X = dir.X;
            Dir.Y = dir.Y;
        }
        
        public void Restart(Point pos, Point dir, Size size)
        {
            Restart(pos, dir);
            Size.Width = size.Width;
            Size.Height = size.Height;
        }

        public void ToLog(object o, string msg)
        {
            if (logStr != null) logStr(o, msg);
        }
        
    }
}