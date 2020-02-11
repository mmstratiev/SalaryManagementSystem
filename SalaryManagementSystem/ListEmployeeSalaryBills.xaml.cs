using System;
using System.Collections.Generic;
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

        List<EmployeeSalaryBill> salaryBills;

        public ListEmployeeSalaryBills()
        {
            InitializeComponent();

            using (var db = new EmployeesDBContext())
            {
                //var tempEmp = (from e in db.Employees select e).FirstOrDefault<Employee>();
                //var tempSalaryBill = new EmployeeSalaryBill() { Date = DateTime.Now, Employee = tempEmp };
                //db.EmployeeSalaryBills.Add(tempSalaryBill);
                //db.SaveChanges();
                var employees = db.Employees.ToList();
                var bills = db.EmployeeSalaryBills.ToList();
                salaryBills = bills;
            }
            billsDataBinding.ItemsSource = salaryBills;
        }
    }
}
