using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Employee : INotifyPropertyChanged
    {
        string name;
        Department dep;

        public int Id { get; set; }
        //public string FIO { get; set; }
        public Nullable<int> Department { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Employee()
        {
            name = String.Empty;
            dep = null;
            Department = null;
        }

        public Employee(string _name)
        {
            name = _name;
            dep = null;
            Department = null;
        }

        public Employee(string _name, Department _department)
        {
            name = _name;
            dep = _department;
            Department = dep.Id;
        }

        public override string ToString()
        {
            return String.Format("{0} из ({1})", name, (Department == null) ? "ниоткуда" : Department.ToString());
        }

        public string FIO
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        public Department DepName
        {
            get { return dep; }
            set { dep = value; Department = dep.Id; NotifyPropertyChanged(); }
        }
    }
}
