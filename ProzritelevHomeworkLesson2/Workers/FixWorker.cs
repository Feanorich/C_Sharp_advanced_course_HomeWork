using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class FixWorker : Worker
    {
        public FixWorker(string name, double payRate) : base(name, payRate)
        {

        }

        public override double Payroll()
        {
            return payRate;
        }
    }
}
