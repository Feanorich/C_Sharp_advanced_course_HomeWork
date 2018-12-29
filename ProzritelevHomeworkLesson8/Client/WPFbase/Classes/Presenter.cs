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
            Log.Msg("создаем презентер");

            this.view = View;
            model = new Model();

            //model.DefaultData();
            model.LoadData();
            Log.Msg("привязываем коллекции");
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
                editWorker.WName = w.FIO;
                editWorker.SelectDepartment.SelectedItem = DataBase.FindDep(w.Department);
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
                Upd();
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
            Upd();
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public void DelW()
        {
            //int ind = view.ColWorkers.SelectedIndex;
            //DataBase.workers.Remove((Employee)view.ColWorkers.SelectedItem);
            model.DelWorker((Employee)view.ColWorkers.SelectedItem);
            Upd();
            
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
            Upd();
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
                Department D = (Department)view.ColDepartments.SelectedItem;
                model.EditDep(D, newName);
            }
            Upd();
        }

        public void Upd()
        {
            model.LoadData();
            view.ColWorkers.ItemsSource = DataBase.workers;
            view.ColDepartments.ItemsSource = DataBase.departments;
        }

        public void Default()
        {
            model.DefaultData();
            Upd();
        }
    }
}
