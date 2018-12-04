using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{    
    abstract class Worker : IComparable
    {
        public string name;
        public double payRate;

        protected Worker(string name, double payRate)
        {
            this.name = name;
            this.payRate = payRate;
        }

        public override string ToString()
        {
            return String.Format("Имя: {0}  Зарплата: {1:f2}", name, Payroll());
        }

        /// <summary>
        /// Расчет среднемесячной заработной платы
        /// </summary>
        /// <param name="payRate">ставка</param>
        /// <returns>среднемесячная зарплата</returns>
        public abstract double Payroll();

        public int CompareTo(object obj)
        {
            Double pr1 = Payroll();
            Double pr2 = ((Worker)obj).Payroll();
            if (pr1 < pr2) return -1;
            if (pr1 > pr2) return 1;
            return 0;
        }
    }
}
