/* Прозрителев Александр
 * 
 * 2. Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в 
 * данной коллекции:
 * a. для целых чисел;
 * b. * для обобщенной коллекции;
 * c. ** используя Linq. (как применить это, в данной задаче, я не осилил :( )
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
            Console.WriteLine("Коллекция чисел");

            List<int> l1 = new List<int> { 1, 2, 3, 4, 5, 2, 1, 3, 1 };

            NumOfEnrties(l1);

            Console.WriteLine("\nКоллекция цветов");

            List<ConsoleColor> l2 = new List<ConsoleColor>
            {
                ConsoleColor.Black,
                ConsoleColor.Blue,
                ConsoleColor.Cyan,
                ConsoleColor.Black,
                ConsoleColor.Blue,
                ConsoleColor.Black
            };

            NumOfEnrties(l2);

            Console.ReadLine();
        }

        public static void NumOfEnrties<T>(List<T> col)
        {
            Dictionary<T, int> dict = new Dictionary<T, int>();

            foreach (var i in col)
            {
                if (dict.ContainsKey(i))
                {
                    dict[i]++;
                }
                else
                {
                    dict.Add(i, 1);
                }
            }

            foreach (var i in dict)
            {
                Console.WriteLine(string.Format("значение: {0} встречается раз: {1}", 
                    i.Key.ToString(), i.Value.ToString()));
            }


        }
    }
}