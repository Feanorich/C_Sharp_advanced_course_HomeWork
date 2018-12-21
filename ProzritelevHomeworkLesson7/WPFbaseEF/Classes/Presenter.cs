using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            
            //мутим SQL
            DataBase.MyDB.Workers.Load();
            view.ColWorkers.ItemsSource = 
                DataBase.MyDB.Workers.Local.ToBindingList();

            DataBase.MyDB.Departments.Load();
            view.ColDepartments.ItemsSource =
                DataBase.MyDB.Departments.Local.ToBindingList();
        }
        /// <summary>
        /// Открывает диалог редактирования сотрудника
        /// </summary>
        /// <param name="w">Редактируемый сотрудник</param>
        public EditWorker ShowEditDialog(Workers w = null)
        {
            EditWorker editWorker = new EditWorker();
            editWorker.SelectDepartment.ItemsSource = DataBase.MyDB.Departments.Local.ToBindingList();
            if (w != null)
            {
                editWorker.WName = w.FIO;
                
                editWorker.SelectDepartment.SelectedItem = DataBase.MyDB.Departments.Find(w.Department);
            }
            editWorker.ShowDialog();
            return editWorker;
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        public void EditWorker()
        {
            int Id = ((Workers)view.ColWorkers.SelectedItem).Id; 
            if (Id >= 0)
            {
                Workers w = DataBase.MyDB.Workers.Find(Id);

                EditWorker editWorker = ShowEditDialog(w);

                if (editWorker.DialogResult == true)
                {
                    Departments NewDep =
                    (Departments)editWorker.SelectDepartment.SelectedItem;

                    model.EditWorker(w, editWorker.WName, NewDep);   
                    
                }

            }
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        public void AddW()
        {      
            
            EditWorker editWorker = ShowEditDialog();

            if (editWorker.DialogResult == true)
            {
                model.NewWorker(editWorker.WName,
                (Departments)editWorker.SelectDepartment.SelectedItem);
            }
                       
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public void DelW()
        {
            Workers w = (Workers)view.ColWorkers.SelectedItem;

            if (w != null)
            {
                DataBase.MyDB.Workers.Remove(w);
                DataBase.MyDB.SaveChanges();
            }           
        }
        /// <summary>
        /// Добавление/удаление дупартаментов
        /// </summary>
        public void EditColD()
        {
            int ind = view.ColDepartments.SelectedIndex;
            if (ind >= 0)
            {
                Departments d = (Departments)view.ColDepartments.SelectedItem;

                model.RemoveDep(d);           
            }
            else
            {                
                model.NewDep();
            }
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        public void NewDepartment()
        {
            model.NewDep();
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void RemoveDepartment()
        {
            Departments d = (Departments)view.ColDepartments.SelectedItem;
            if (d != null)
            {
                model.RemoveDep(d);
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
                Departments D = (Departments)view.ColDepartments.SelectedItem;
                model.EditDep(D, newName);
            }
        }

        public void Default()
        {
            model.DefaultData();
        }
        
    }
}
