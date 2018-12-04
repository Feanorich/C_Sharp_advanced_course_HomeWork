using System;

namespace Workers
{
    class Program
    {
        static void PrintW(Worker[] w)
        {
            foreach (Worker item in w)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void Main(string[] args)
        {
            Worker[] w = new Worker[4];

            Console.WriteLine("Заполним массив");

            w[0] = new FixWorker("Ivanov", 110000);
            w[1] = new FixWorker("Petrov", 100000);
            w[2] = new TimeWorker("Sidoruv", 600);
            w[3] = new TimeWorker("Pupkin", 555);
            PrintW(w);

            Console.WriteLine("\nОтсортируем массив по среднемесячной зп");           

            Array.Sort(w);
            PrintW(w);

            Console.WriteLine("\nСоздадим объект со списком");

            Staff st = new Staff(w);

            Console.WriteLine(st.ToString());

            Console.WriteLine("\nВыведем циклом foreach");

            foreach (Worker item in st)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
            
        }
    }
}