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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFbase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            App.DefaultData();

            ListWorkers.ItemsSource = App.workers;
            ListDepartments.ItemsSource = App.departments;
        }

        private void workers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

            int ind = ListWorkers.SelectedIndex; //App.workers.IndexOf((Employee)workers.SelectedItem);
            if (ind >= 0)
            {
                Employee w = App.workers[ind];

                EditWorker editWorker = new EditWorker(w);                
                editWorker.Owner = this;                         
                editWorker.ShowDialog();
            }
                    
            
        }

        private void delW_Click(object sender, RoutedEventArgs e)
        {
            int ind = ListWorkers.SelectedIndex;
            App.workers.Remove((Employee) ListWorkers.SelectedItem);
        }

        private void addW_Click(object sender, RoutedEventArgs e)
        {
            EditWorker editWorker = new EditWorker();
            editWorker.Owner = this;
            editWorker.ShowDialog();
        }

        private void nameD_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            int ind = ListDepartments.SelectedIndex;
            if (ind >= 0)
            {
                Department D = App.departments[ind];
                ((Department)ListDepartments.SelectedItem).Name = ((TextBox)sender).Text;
                D.Name = ((TextBox)sender).Text;
                //MessageBox.Show(App.departments[ind].Name);
            }
        }

        private void ListDepartments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int ind = ListDepartments.SelectedIndex;
            if (ind >= 0)
            {
                Department d = (Department)ListDepartments.SelectedItem;
                foreach (var w in App.workers)
                {
                    if (w.Department == d) w.Department = null;
                }
                App.departments.Remove(d);
            }
            else
            {
                App.departments.Add(new Department("новый отдел"));
                
            }
        }
    }
}
