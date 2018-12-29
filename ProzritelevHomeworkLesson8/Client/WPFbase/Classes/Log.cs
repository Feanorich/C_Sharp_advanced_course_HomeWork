using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase.Classes
{
    
    public static class Log
    {
        public static bool debug = false;

        public static void Msg(string msg)
        {
            if (debug)
            {
                Console.WriteLine(msg);
            }            
        }

    }
}
