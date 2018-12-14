using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFbase
{
    /// <summary>
    /// Логика взаимодействия для EditWorker.xaml
    /// </summary>
    public partial class EditWorker : Window
    {
        public Employee CurrentWorker { get; set; }

        public EditWorker()
        {
            InitializeComponent();
            WorkerDep.ItemsSource = App.departments;
        }

        public EditWorker(Employee _CurrentWorker):this()
        {            
            CurrentWorker = _CurrentWorker;

            WorkerName.Text = CurrentWorker.Name;            
            WorkerDep.SelectedItem = CurrentWorker.Department;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentWorker == null)
            {
                if (WorkerName.Text != String.Empty)
                {
                    App.workers.Add(new Employee(WorkerName.Text, (Department)WorkerDep.SelectedItem));
                }
            }
            else
            {
                int ind = App.workers.IndexOf(CurrentWorker);

                if (CurrentWorker.Name != WorkerName.Text) CurrentWorker.Name = WorkerName.Text;
                if (CurrentWorker.Department != (Department)WorkerDep.SelectedItem)
                    CurrentWorker.Department = (Department)WorkerDep.SelectedItem;

                //foreach (var item in App.workers)
                //{
                //    Console.WriteLine(item.ToString());
                //}
            }
            this.Close();
        }
    }
}
