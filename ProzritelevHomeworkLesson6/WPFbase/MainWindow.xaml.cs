﻿using System;
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
using WPFbase.Classes;

namespace WPFbase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;

        #region Свойствыа для интерфейса IView
        public object SelWorker
        {
            get => ListWorkers.SelectedItem;
            set => ListWorkers.SelectedItem = value;
        }
        public object SelDepartment
        {
            get => ListDepartments.SelectedItem;
            set => ListDepartments.SelectedItem = value;
        }
        public ListBox ColWorkers
        {
            get => ListWorkers;
        }
        public ListBox ColDepartments
        {
            get => ListDepartments;
        }
        public Window MWindow
        {
            get => StartWindow;
        }
        public Grid MGrid
        {
            get => MainGrid;
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            p = new Presenter(this);            

            ListWorkers.MouseDoubleClick += delegate { p.EditWorker(); };  
            btnAddW.Click += delegate { p.AddW(); };
            btnDelW.Click += delegate { p.DelW(); };

            ListDepartments.MouseDoubleClick += delegate { p.EditColD(); };

        }               

        private void nameD_TextChanged(object sender, TextChangedEventArgs e)
        {
            p.EditDepartment(((TextBox)sender).Text);            
        }      
        
    }
}
