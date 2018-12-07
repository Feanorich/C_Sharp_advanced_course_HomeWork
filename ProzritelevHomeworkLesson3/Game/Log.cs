using System;
using System.Drawing;
namespace MyGame
{
    class Log
    {
        int hit;
        int destroyed;

        public Log()
        {
            hit = 0;
            destroyed = 0;
        }

        public void UpScore(object o, string msg)
        {
            if (o is Bullet) 
            {
                hit++;
            }
            else if (o is Asteroid)
            {
                destroyed++;
            }  

        }

        public void WriteLog(object o, string msg)
        {
            string message = String.Format("[{0}] - {1} ", DateTime.Now, msg);

            Console.WriteLine(message);

            using (var r = new System.IO.StreamWriter("GameLog.txt", true))
            {
                r.WriteLine(message);
            }
        }        

        public int Hit
        {
            get
            {
                return hit;
            }
        }
        public int Destroyed
        {
            get
            {
                return destroyed;
            }
        }


    }
}