using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFbase
{
    public interface IEdit
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        string WName { get; set; }
        /// <summary>
        /// Коллекция для выбора департамента
        /// </summary>
        ComboBox SelectDepartment { get; }
    }
}
