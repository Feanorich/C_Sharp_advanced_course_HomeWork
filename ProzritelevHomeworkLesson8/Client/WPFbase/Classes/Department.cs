using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase
{
    public class Department : INotifyPropertyChanged
    {
        string name;
        public int Id { get; set; }
        //public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Department()
        {
        }

        public Department(string _name)
        {
            name = _name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(); }
        }

    }
}
