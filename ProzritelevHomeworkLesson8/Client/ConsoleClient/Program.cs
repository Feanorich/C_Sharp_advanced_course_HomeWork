using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            string url = @"http://localhost:52001/getlistw";

            

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));

            var res = client.GetStringAsync(url).Result;            

            Console.WriteLine(res);

            //HttpResponseMessage response = client.GetAsync(url).Result;

            ObservableCollection<Employee> ListDep = new ObservableCollection<Employee>();

            ListDep = GetProductsAsync(url).Result;

            Console.WriteLine("\nДесериализация");

            Console.WriteLine(ListDep);

            foreach (var item in ListDep)
            {
                Console.WriteLine(item.ToString());

            }

            Console.ReadLine();
        }

        static async Task<ObservableCollection<Employee>> GetProductsAsync(string path)
        {
            ObservableCollection<Employee> products = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    products = await
                    response.Content.ReadAsAsync<ObservableCollection<Employee>>();
                }
            }
            catch (Exception)
            {
            }
            return products;
        }
    }
}
