using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Staff : IEnumerable
    {
        public Worker[] workers;
        public Staff(Worker[] w)
        {
            workers = new Worker[w.Length];
            for (int i = 0; i < w.Length; i++)
            {
                if (w[i].GetType() == typeof(TimeWorker))
                {
                    workers[i] = new TimeWorker(w[i].name, w[i].payRate);
                } else if (w[i].GetType() == typeof(FixWorker))
                {
                    workers[i] = new FixWorker(w[i].name, w[i].payRate);
                }                
            }
        }

        public override string ToString()
        {
            string s = string.Empty; 
            for (int i = 0; i < workers.Length; i++)
            {
                if (i != 0) s += "\n";
                s += workers[i].ToString();
            }
            return s;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < workers.Length; i++)
                yield return workers[i];
        }
    }
}
