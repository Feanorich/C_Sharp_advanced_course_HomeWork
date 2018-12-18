using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase.Classes
{
    class Presenter
    {
        private Model model;
        private IView view;

        public Presenter(IView View)
        {
            this.view = View;
            model = new Model();

            model.DefaultData();
                        
            view.ColWorkers.ItemsSource = DataBase.workers;
            view.ColDepartments.ItemsSource = DataBase.departments;
            
        }
        /// <summary>
        /// Открывает диалог редактирования сотрудника
        /// </summary>
        /// <param name="w">Редактируемый сотрудник</param>
        public EditWorker ShowEditDialog(Employee w = null)
        {
            EditWorker editWorker = new EditWorker();
            editWorker.SelectDepartment.ItemsSource = DataBase.departments;
            if (w != null)
            {
                editWorker.WName = w.Name;
                editWorker.SelectDepartment.SelectedItem = w.Department;
            }
            editWorker.ShowDialog();
            return editWorker;
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        public void EditWorker()
        {
            int ind = view.ColWorkers.SelectedIndex; 
            if (ind >= 0)
            {
                Employee w = DataBase.workers[ind];

                EditWorker editWorker = ShowEditDialog(w);

                Department NewDep = 
                    (Department)editWorker.SelectDepartment.SelectedItem;

                model.EditWorker(w, editWorker.WName, NewDep);                
            }
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        public void AddW()
        {
            EditWorker editWorker = ShowEditDialog();

            model.NewWorker(editWorker.WName, 
                (Department)editWorker.SelectDepartment.SelectedItem);            
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public void DelW()
        {
            int ind = view.ColWorkers.SelectedIndex;
            DataBase.workers.Remove((Employee)view.ColWorkers.SelectedItem);
        }
        /// <summary>
        /// Добавление/удаление дупартаментов
        /// </summary>
        public void EditColD()
        {
            int ind = view.ColDepartments.SelectedIndex;
            if (ind >= 0)
            {
                Department d = (Department)view.ColDepartments.SelectedItem;

                model.RemoveDep(d);           
            }
            else
            {                
                model.NewDep();
            }
        }
        /// <summary>
        /// редактирование отдела
        /// </summary>
        /// <param name="newName"></param>
        public void EditDepartment(string newName)
        {
            int ind = view.ColDepartments.SelectedIndex;
            if (ind >= 0)
            {
                Department D = DataBase.departments[ind];
                model.EditDep(D, newName);
            }
        }
    }
}
