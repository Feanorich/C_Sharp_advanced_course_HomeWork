/* Прозрителев Александр
 * 
 * 5. * Добавить в пример Lesson3 обобщенный делегат
 * 
 * Или я не понял суть задания, или я не понял почему оно со звездочкой :/
 */

using System;
namespace Delegates_Observer
{
    //public delegate void MyDelegate(object o);    //уберем описание делегата
    class Source
    {
        //public event MyDelegate Run;  //заменим описание события
        public event Action<object> Run;

        public void Start()
        {
            Console.WriteLine("RUN");
            if (Run != null) Run(this);
        }
    }
    class Observer1 // Наблюдатель 1
    {
        public void Do(object o)
        {
            Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
        }
    }
    class Observer2 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();

            //MyDelegate d1 = new MyDelegate(o1.Do);    //Создадим экземпляр не своего, а обобщенного делегата    
            Action<object> d1 = new Action<object>(o1.Do);

            s.Run += d1;
            s.Run += o2.Do;
            s.Start();
            s.Run -= d1;
            s.Start();

            Console.ReadLine();
        }
    }
}