using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFbase
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// список работников
        /// </summary>
        public static ObservableCollection<Employee> workers = new ObservableCollection<Employee>();
        /// <summary>
        /// список отделов
        /// </summary>
        public static ObservableCollection<Department> departments = new ObservableCollection<Department>();

        /// <summary>
        /// первичное заполнение коллекций
        /// </summary>
        public static void DefaultData()
        {
            //заполним отделы
            Department management = new Department("Управленческий отдел");
            Department personnel = new Department("Кадровый отдел");
            App.departments.Add(management);
            App.departments.Add(personnel);

            //заполним чуваков
            App.workers.Add(new Employee("Иванов", management));
            App.workers.Add(new Employee("Петров", personnel));
            App.workers.Add(new Employee("Сидоров"));

        }

    }


}
