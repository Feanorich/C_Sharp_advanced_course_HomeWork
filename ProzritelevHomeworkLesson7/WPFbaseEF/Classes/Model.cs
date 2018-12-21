﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            if (DataBase.MyDB.Departments.Count() == 0)
            {
                Departments Men = new Departments() { Name = "Управленческий отдел" };
                Departments Fin = new Departments() { Name = "Финансовый отдел" };
                Departments Hr = new Departments() { Name = "Отдел персонала" };

                DataBase.MyDB.Departments.Add(Men);                
                DataBase.MyDB.Departments.Add(Fin);                
                DataBase.MyDB.Departments.Add(Hr);
                DataBase.MyDB.SaveChanges();

                if (DataBase.MyDB.Workers.Count() == 0)
                {
                    DataBase.MyDB.Workers.Add(new Workers() { FIO = "Александр Гайнанов", Department = Men.Id });
                    DataBase.MyDB.Workers.Add(new Workers() { FIO = "Марина Волкова", Department = Fin.Id });
                    DataBase.MyDB.Workers.Add(new Workers() { FIO = "Людмила Симонова", Department = Hr.Id });
                    DataBase.MyDB.Workers.Add(new Workers() { FIO = "Андрей Клименко", Department = null });
                    DataBase.MyDB.SaveChanges();
                }
            }

            
        }
        /// <summary>
        /// Добавляет нового сотрудника в базу
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="dep">Департамент сотрудника</param>
        public void NewWorker(string name, Departments dep)
        {
            if (name != String.Empty)
            {
                DataBase.MyDB.Workers.Add(new Workers(name, dep));
                DataBase.MyDB.SaveChanges();
            }
        }
        /// <summary>
        /// Изменяет данные о сотруднике в базе
        /// </summary>
        /// <param name="w">Редактируемый сотрудник</param>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="dep">Департамент сотрудника</param>
        public void EditWorker(Workers w, string name, Departments dep)
        {
            if (name != String.Empty)
            {
                if (w.FIO != name)
                { w.FIO = name; }

                if (dep == null) w.Department = null;
                else
                {
                    if (w.Department != dep.Id)
                    { w.Department = dep.Id; }
                }
                DataBase.MyDB.SaveChanges();
            }
        }
        /// <summary>
        /// Добавление нового департамента в базу
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        public void NewDep(string name = "новый отдел")
        {
            DataBase.MyDB.Departments.Add(new Departments() { Name = name });
            DataBase.MyDB.SaveChanges();
        }
        /// <summary>
        /// Удаляет департамент из базы данных
        /// </summary>
        /// <param name="d">Удаляемый департамент</param>
        public void RemoveDep(Departments d)
        {
            foreach (var w in DataBase.MyDB.Workers)
            {
                if (w.Department == d.Id) w.Department = null;
            }

            DataBase.MyDB.Departments.Remove(d);
            DataBase.MyDB.SaveChanges();            
        }
        /// <summary>
        /// Редактирование данных о департаменте в базе
        /// </summary>
        /// <param name="D">Редактируемый департамент</param>
        /// <param name="newName">Новое название</param>
        public void EditDep(Departments D, string newName)
        {
            D.Name = newName;
            DataBase.MyDB.SaveChanges();
        }
        
    }
}
