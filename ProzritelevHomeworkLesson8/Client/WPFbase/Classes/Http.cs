using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WPFbase.Classes
{
    class Http
    {
        /// <summary>
        /// HTTP клиент
        /// </summary>
        public static HttpClient client = new HttpClient();
        /// <summary>
        /// url для получения списка департаментов
        /// </summary>
        public static string urlGetD = @"http://localhost:52001/getlistd";
        /// <summary>
        /// url для получения списка сотрудников
        /// </summary>
        public static string urlGetW = @"http://localhost:52001/getlistw";
        /// <summary>
        /// url для добавления департамента
        /// </summary>
        public static string urlAddD = @"http://localhost:52001/adddep";
        /// <summary>
        /// url для удаления департамента
        /// </summary>
        public static string urlDelD = @"http://localhost:52001/deldep";
        /// <summary>
        /// url для редактирования департамента
        /// </summary>
        public static string urlEditD = @"http://localhost:52001/editdep";
        /// <summary>
        /// url для добавления сотрудника
        /// </summary>
        public static string urlAddW = @"http://localhost:52001/addw";
        /// <summary>
        /// url для редактирования сотрудника
        /// </summary>
        public static string urlEditW = @"http://localhost:52001/editw";
        /// <summary>
        /// url для удаления сотрудника
        /// </summary>
        public static string urlDelW = @"http://localhost:52001/delw";
        /// <summary>
        /// url для первичного заполнения
        /// </summary>
        public static string urlDD = @"http://localhost:52001/defaultdata";

        static Http()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}


//{"Id":30,"FIO":"Александр Гайнанов","Department":27}
