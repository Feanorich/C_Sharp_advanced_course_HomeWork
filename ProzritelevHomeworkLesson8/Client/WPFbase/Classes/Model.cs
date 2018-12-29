using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace WPFbase.Classes
{
    class Model
    {       

        public Model()
        {
            Log.Msg("создаем модель");
        }

        static async Task<ObservableCollection<T>> GetListAsync<T>(string path)
        {
            ObservableCollection<T> products = null;
            try
            {
                HttpResponseMessage response = Http.client.GetAsync(path).Result;
                if (response.IsSuccessStatusCode)
                {
                    products = 
                    response.Content.ReadAsAsync<ObservableCollection<T>>().Result;
                }
            }
            catch (Exception)
            {
                Log.Msg("Не удалось загрузить список " + products.GetType());
            }
            return products;
        }        

        public void LoadData()
        {
            Log.Msg("загружаем базу");

            DataBase.departments.Clear();
            DataBase.workers.Clear();            
                        
            DataBase.departments = GetListAsync<Department>(Http.urlGetD).Result;
                        
            DataBase.workers = GetListAsync<Employee>(Http.urlGetW).Result;
        }

        /// <summary>
        /// первичное заполнение коллекций
        /// </summary>
        public void DefaultData()
        {            
            MakePost("", Http.urlDD);
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
                //DataBase.workers.Add(new Employee(name, dep));

                try
                {
                    string StrW = @"{ 'Id':0,'FIO':'" + name + @"','Department':"+dep.Id+"}";
                                        
                    MakePost(StrW, Http.urlAddW);

                }
                catch (Exception)
                {

                }

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
            if (w != null)
            {
                if (name != String.Empty)
                {
                    Nullable<int> dId = (dep == null) ? 0 : dep.Id;
                    string StrW = @"{ 'Id':" + w.Id + ",'FIO':'" + name + @"','Department':" + dId + "}";

                    MakePost(StrW, Http.urlEditW);

                }
            }
            
        }
        /// <summary>
        /// Удаление сотрудника из базы
        /// </summary>
        /// <param name="w">Удаляемый сотрудник</param>
        public void DelWorker(Employee w)
        {
            if (w != null)
            {
                string StrW = @"{ 'Id':" + w.Id + ",'FIO':'" + w.FIO + @"','Department':" + w.Department + "}";

                MakePost(StrW, Http.urlDelW);
            }
            
        }
        /// <summary>
        /// Добавление нового департамента в базу
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        public void NewDep(string name = "новый отдел")
        {
            Department ND = new Department(name);
            //DataBase.departments.Add(ND);

            try
            {
                string StrDep = @"{ 'Id':0,'Name':'"+ name + @"'}";                

                MakePost(StrDep, Http.urlAddD);

            }
            catch (Exception)
            {
                
            }
            
        }
        /// <summary>
        /// Удаляет департамент из базы данных
        /// </summary>
        /// <param name="d">Удаляемый департамент</param>
        public void RemoveDep(Department d)
        {
            string StrDep = @"{ 'Id':" + d.Id + @",'Name':'" + d.Name + @"'}";            

            MakePost(StrDep, Http.urlDelD);
        }
        /// <summary>
        /// Редактирование данных о департаменте в базе
        /// </summary>
        /// <param name="D">Редактируемый департамент</param>
        /// <param name="newName">Новое название</param>
        public void EditDep(Department D, string newName)
        {
            
            //D.Name = newName;
            string StrDep = @"{ 'Id':" + D.Id + @",'Name':'" + newName + @"'}";

            MakePost(StrDep, Http.urlEditD);

        }

        public HttpResponseMessage MakePost(string StrCont, string url)
        {
            StringContent cont = new StringContent(StrCont, Encoding.UTF8, "application/json");
            HttpResponseMessage response = Http.client.PostAsync(url, cont).Result;
            Log.Msg(response.StatusCode.ToString());

            return response;
        }
    }
}
