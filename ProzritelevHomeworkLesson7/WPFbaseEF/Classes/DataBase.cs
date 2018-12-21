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
        /// EF объект с нашей базой
        /// </summary>
        public static WPFbaseEntities MyDB = new WPFbaseEntities();        

        public static Departments DepConverter(object value)
        {
            Departments dep = DataBase.MyDB.Departments.Find(value);
            if (value == null)
            {
                return null;
            }
            if (dep == null)
            {
                if ((int)value > 0) return null;
                else return null;
            }
            else return dep;
        }
    }
}
