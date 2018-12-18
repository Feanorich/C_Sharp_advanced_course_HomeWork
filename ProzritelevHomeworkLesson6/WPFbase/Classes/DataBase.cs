using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase.Classes
{
    /// <summary>
    /// База данных с работниками и департаментами
    /// </summary>
    class DataBase
    {
        /// <summary>
        /// список работников
        /// </summary>
        public static ObservableCollection<Employee> workers =
            new ObservableCollection<Employee>();
        /// <summary>
        /// список отделов
        /// </summary>
        public static ObservableCollection<Department> departments =
            new ObservableCollection<Department>();
    }
}
