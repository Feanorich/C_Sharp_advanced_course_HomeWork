/* Прозрителев Александр
 * 
 * 3. * Дан фрагмент программы:
 * а) Свернуть обращение к OrderBy с использованием лямбда-выражения 
 * б) *Развернуть обращение к OrderBy с использованием делегата 
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four" , 4 },
                { "two" , 2 },
                { "one" , 1 },
                { "three" , 3 },
            };

            //оригинальная строка
            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            //а) Свернуть обращение к OrderBy с использованием лямбда-выражения 
            //var d = dict.OrderBy(pair => pair.Value );

            //б) *Развернуть обращение к OrderBy с использованием делегата            
            var d = dict.OrderBy(MyValue);       

            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.ReadLine();
        }

        public static int MyValue(KeyValuePair<string, int> pair)
        {
            return pair.Value;
        }
    }
}