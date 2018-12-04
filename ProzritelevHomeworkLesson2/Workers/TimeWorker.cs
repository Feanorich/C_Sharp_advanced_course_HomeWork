using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class TimeWorker : Worker
    {
        public TimeWorker(string name, double payRate) : base(name, payRate)
        {

        }

        public override double Payroll()
        {
            return 20.8 * 8 * payRate;
        }
        
    }
}
