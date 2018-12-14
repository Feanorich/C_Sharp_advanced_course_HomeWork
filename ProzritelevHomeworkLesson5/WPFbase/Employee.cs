using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase
{
    public class Employee : INotifyPropertyChanged
    {
        string name;
        Department department;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));            
        }

        public Employee()
        {
            name = String.Empty;
            department = null;
        }

        public Employee(string _name)
        {
            name = _name;
            department = null;
        }

        public Employee(string _name, Department _department)
        {
            name = _name;
            department = _department;
        }

        public override string ToString()
        {
            return String.Format("{0} из {1}", name, (department == null) ? "ниоткуда" : department.Name);
        }

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

        public Department Department
        {
            get { return department; }
            set { department = value; NotifyPropertyChanged(); }
        }
    }
}
