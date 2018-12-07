using System.Drawing;
namespace MyGame
{
    class Ship : BaseObject
    {
        private int maxEnergy = 50;
        private int _energy;
        public int Energy => _energy;

        public static event Message MessageDie;

        public void EnergyLow(BaseObject bo, int n)
        {
            int damage = n * 2;
            _energy -= damage;
            if (_energy > maxEnergy) _energy = maxEnergy;
            
            if (n > 0)
            {
                ToLog(bo, string.Format("корабль получил урон: {0} осталось энергии: {1}", damage, _energy));
            }
            else if (n < 0)
            {
                ToLog(bo, string.Format("аптечка на {0} энергии, теперь: {1}", -damage, _energy));
            }
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _energy = maxEnergy;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Cyan, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
            ToLog(this, "корабль уничтожен");
        }
    }
}