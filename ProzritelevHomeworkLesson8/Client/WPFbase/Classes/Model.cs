using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase.Classes
{
    class Model
    {
        public Model()
        {
        }
        /// <summary>
        /// первичное заполнение коллекций
        /// </summary>
        public void DefaultData()
        {
            //создадим экземпляры отделов
            Department management = new Department("Управленческий отдел");
            Department personnel = new Department("Кадровый отдел");
            //заполним отделы
            DataBase.departments.Add(management);
            DataBase.departments.Add(personnel);
            //заполним чуваков
            DataBase.workers.Add(new Employee("Иванов", management));
            DataBase.workers.Add(new Employee("Петров", personnel));
            DataBase.workers.Add(new Employee("Сидоров"));
        }
        /// <summary>
        /// Добавляет нового сотрудника в базу
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="dep">Департамент сотрудника</param>
        public void NewWorker(string name, Department dep)
        {
            if (name != String.Empty)
            {
                DataBase.workers.Add(new Employee(name, dep));
            }
        }
        /// <summary>
        /// Изменяет данные о сотруднике в базе
        /// </summary>
        /// <param name="w">Редактируемый сотрудник</param>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="dep">Департамент сотрудника</param>
        public void EditWorker(Employee w, string name, Department dep)
        {
            if (name != String.Empty)
            {
                if (w.Name != name)
                { w.Name = name; }

                if (w.Department != dep)
                { w.Department = dep; }
            }
        }
        /// <summary>
        /// Добавление нового департамента в базу
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        public void NewDep(string name = "новый отдел")
        {
            DataBase.departments.Add(new Department(name));
        }
        /// <summary>
        /// Удаляет департамент из базы данных
        /// </summary>
        /// <param name="d">Удаляемый департамент</param>
        public void RemoveDep(Department d)
        {            
            foreach (var w in DataBase.workers)
            {
                if (w.Department == d) w.Department = null;
            }
            DataBase.departments.Remove(d);
        }
        /// <summary>
        /// Редактирование данных о департаменте в базе
        /// </summary>
        /// <param name="D">Редактируемый департамент</param>
        /// <param name="newName">Новое название</param>
        public void EditDep(Department D, string newName)
        {
            D.Name = newName;
        }
    }
}
