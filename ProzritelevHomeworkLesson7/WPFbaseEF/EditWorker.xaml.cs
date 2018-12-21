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
using WPFbase.Classes;

namespace WPFbase
{
    /// <summary>
    /// Логика взаимодействия для EditWorker.xaml
    /// </summary>
    public partial class EditWorker : Window, IEdit
    {
        public Employee CurrentWorker { get; set; }

        /// <summary>
        /// Имя работника
        /// </summary>
        public string WName
        {
            get => WorkerName.Text;
            set => WorkerName.Text = value;
        }
        /// <summary>
        /// Коллекция для выбора департамента
        /// </summary>
        public ComboBox SelectDepartment
        {
            get => WorkerDep;
        }

        public EditWorker()
        {
            InitializeComponent();            
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
