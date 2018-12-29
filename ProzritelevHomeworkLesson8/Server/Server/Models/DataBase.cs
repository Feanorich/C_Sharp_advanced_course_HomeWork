using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Server.Models
{
    /// <summary>
    /// База данных с работниками и департаментами
    /// </summary>
    class DataBase
    {
        public static WPFbaseEntities MyDB = new WPFbaseEntities();

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

        static DataBase()
        {
            DefaultData();
        }

        /// <summary>
        /// первичное заполнение коллекций
        /// </summary>        
        public static bool DefaultData()
        {
            
            try
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
            catch
            {
                return false;
            }
            return true;
        }

        public static bool AddDep(Departments Dep)
        {
            try
            {
                DataBase.MyDB.Departments.Add(new Departments() { Name = Dep.Name });
                DataBase.MyDB.SaveChanges();                
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool DelDep(Departments Dep)
        {
            try
            {
                foreach (var w in DataBase.MyDB.Workers)
                {
                    if (w.Department == Dep.Id)
                    {
                        w.Department = null;
                    }
                }
                DataBase.MyDB.Departments.Remove(FindDep(Dep.Id));
                DataBase.MyDB.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool EditDep(Departments Dep)
        {
            try
            {
                Departments D = FindDep(Dep.Id);
                if (D != null)
                {
                    D.Name = Dep.Name;
                    DataBase.MyDB.SaveChanges();
                } else return false;

            }
            catch
            {
                return false;
            }
            return true;
        }

        public static Departments FindDep(Nullable<int> id)
        {
            //Departments res = null;

            return DataBase.MyDB.Departments.Find(id);

            //foreach (var i in DataBase.MyDB.Departments)
            //{
            //    if (i.Id == id)
            //    {
            //        return i;
            //    }
            //}

            //return res;
        }

        public static bool AddWorker(Workers W)
        {
            try
            {
                DataBase.MyDB.Workers.Add(new Workers() { FIO = W.FIO, Department = W.Department });
                DataBase.MyDB.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool DelWorker(Workers W)
        {
            try
            {                
                DataBase.MyDB.Workers.Remove(DataBase.MyDB.Workers.Find(W.Id));
                DataBase.MyDB.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool EditWorker(Workers W)
        {
            try
            {
                var E = DataBase.MyDB.Workers.Find(W.Id);
                if (E != null)
                {
                    int edit = 0;
                    if (E.FIO != W.FIO)
                    { E.FIO = W.FIO; edit = 1; }

                    if (E.Department != W.Department)
                    { E.Department = W.Department; edit = 1; }
                    if (edit == 1)
                    {
                        DataBase.MyDB.SaveChanges();
                    }                    
                }
                
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}