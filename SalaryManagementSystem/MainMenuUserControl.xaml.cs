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

namespace SalaryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainMenuUserControl.xaml
    /// </summary>
    public partial class MainMenuUserControl : UserControl
    {
        public MainMenuUserControl()
        {
            InitializeComponent();
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetUserControl(new AddEmployeeUserControl());
        }

        private void FindEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetUserControl(new FindEmployeeUserControl());
        }

        private void ShowSalaryBills_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetUserControl(new ListEmployeeSalaryBills());
        }
    }
}
