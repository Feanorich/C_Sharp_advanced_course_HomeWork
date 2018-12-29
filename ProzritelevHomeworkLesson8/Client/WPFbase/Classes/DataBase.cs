using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        public static int editedDep = 0;

        public static Department FindDep(Nullable<int> id)
        {
            Department res = null;

            foreach (var i in DataBase.departments)
            {
                if (i.Id == id)
                {
                    return i;
                }
            }

            return res;
        }

        public static string DepConverter(object value)
        {
            
            if (value == null)
            {
                return null;
            }
            Department dep = FindDep((Nullable<int>)value);
            if (dep == null)
            {
                if ((int)value > 0) return null;
                else return null;
            }
            else return dep.Name;
        }

    }
}
