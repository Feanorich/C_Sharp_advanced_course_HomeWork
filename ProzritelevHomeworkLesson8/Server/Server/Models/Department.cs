using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Department  
    {
        string name;

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

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
            set { name = value; /*NotifyPropertyChanged();*/ }
        }

    }
}