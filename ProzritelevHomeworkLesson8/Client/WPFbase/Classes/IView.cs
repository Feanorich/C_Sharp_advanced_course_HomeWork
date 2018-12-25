using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFbase.Classes
{
    public interface IView
    {
        object SelWorker { get; set; }
        object SelDepartment { get; set; }
        ListBox ColWorkers { get; }
        ListBox ColDepartments { get; }
        Window MWindow { get; }
        Grid MGrid { get; }
    }
}
