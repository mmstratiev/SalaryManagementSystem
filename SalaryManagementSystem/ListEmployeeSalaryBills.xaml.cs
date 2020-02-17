using SalaryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ListEmployeeSalaryBills : UserControl
    {
        List<EmployeeSalaryBill>    salaryBills;
        ListViewColumnSorter        listViewColumnSorter;

        public ListEmployeeSalaryBills()
        {
            InitializeComponent();

            listViewColumnSorter = new ListViewColumnSorter(listView);

            using (var db = new EmployeesDBContext())
            {
                var employees = db.Employees.ToList();
                var bills = db.EmployeeSalaryBills.ToList();
                salaryBills = bills;
            }
            listView.ItemsSource = salaryBills;
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            listViewColumnSorter.HandleColumnClick(sender, e);
        }
    }
}
