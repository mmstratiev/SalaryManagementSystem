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
using System.IO;
using Microsoft.VisualBasic;
using SalaryManagementSystem.Model;

namespace SalaryManagementSystem
{
    /// <summary>
    /// Interaction logic for FindEmployeeUserControl.xaml
    /// </summary>

    public partial class FindEmployeeUserControl : UserControl
    {
        private List<Employee>  searchResult;
        ListViewColumnSorter    listViewColumnSorter;
        public FindEmployeeUserControl()
        {
            InitializeComponent();

            listViewColumnSorter = new ListViewColumnSorter(EmployeesListView);
        }

        private void RefreshListView()
        {
            using (var db = new EmployeesDBContext())
            {
                if (SearchByComboBox.Text == SalaryManagementSystem.Properties.Resources.NameStr)
                {
                    searchResult = (from em in db.Employees
                                    where em.Name.Contains(SearchTextBox.Text)
                                    select em).ToList();
                }
                else if (SearchByComboBox.Text == SalaryManagementSystem.Properties.Resources.EGNStr)
                {
                    searchResult = (from em in db.Employees
                                    where em.EGN.Contains(SearchTextBox.Text)
                                    select em).ToList();
                }
                else if (SearchByComboBox.Text == SalaryManagementSystem.Properties.Resources.CompanyNameStr)
                {
                    searchResult = (from em in db.Employees
                                    where em.CompanyName.Contains(SearchTextBox.Text)
                                    select em).ToList();
                }
            }
            EmployeesListView.ItemsSource = searchResult;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeesListView.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                using (var db = new EmployeesDBContext())
                {
                    var toEdit = db.Employees.SingleOrDefault(em => em.ID == selectedEmployee.ID);
                    if (toEdit != null)
                    {
                        string newValue = selectedEmployee.Salary.ToString();
                        while (newValue.Length > 0)
                        {
                            newValue = Interaction.InputBox ( SalaryManagementSystem.Properties.Resources.EnterSalaryMsg
                                                            , "Input"
                                                            , newValue);

                            double newValueAsDouble;
                            if (    Double.TryParse(newValue, out newValueAsDouble)
                                &&  newValueAsDouble >= Constants.MIN_SALARY)
                            {
                                toEdit.Salary = newValueAsDouble;
                                db.SaveChanges();
                                RefreshListView();
                                break;
                            }
                            else if(newValue.Length > 0)
                            {
                                string salaryMsg = SalaryManagementSystem.Properties.Resources.InvalidSalaryMsg;
                                salaryMsg = salaryMsg.Replace("^1", Constants.MIN_SALARY.ToString());

                                MessageBox.Show ( salaryMsg
                                                , "Error"
                                                , MessageBoxButton.OK
                                                , MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(SalaryManagementSystem.Properties.Resources.SelectEmployeeMsg);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeesListView.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                using (var db = new EmployeesDBContext())
                {
                    var toRemove = db.Employees.SingleOrDefault(em => em.ID == selectedEmployee.ID);
                    if (toRemove != null)
                    {
                        string deleteMsg = SalaryManagementSystem.Properties.Resources.DeleteEmployeeMsg;
                        deleteMsg = deleteMsg.Replace("^1", selectedEmployee.Name);

                        MessageBoxResult messageBoxResult = MessageBox.Show ( deleteMsg
                                                                            , "Error"
                                                                            , MessageBoxButton.YesNo
                                                                            , MessageBoxImage.Question);

                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            db.Employees.Remove(toRemove);
                            db.SaveChanges();
                            RefreshListView();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(SalaryManagementSystem.Properties.Resources.SelectEmployeeMsg);
            }
        }

        private void CreateBillBtn_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeesListView.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                using (var db = new EmployeesDBContext())
                {
                    var toExport = db.Employees.SingleOrDefault(em => em.ID == selectedEmployee.ID);
                    if (toExport != null)
                    {
                        using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                        {
                            System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                            {
                                EmployeeSalaryBill  salaryBill      = new EmployeeSalaryBill(DateTime.Now, db.Employees.FirstOrDefault(tempE => tempE.ID == selectedEmployee.ID));
                                string fullFilePath = fbd.SelectedPath;
                                if (fbd.SelectedPath.Substring(fbd.SelectedPath.Length - 1) == "\\")
                                {
                                    fullFilePath += "Bill_" + selectedEmployee.Name + "_" + salaryBill.Date.ToString().Replace("/", "_").Replace(":", "_") + ".xlsx";
                                }
                                else
                                {
                                    fullFilePath += "\\Bill_" + selectedEmployee.Name + "_" + salaryBill.Date.ToString().Replace("/", "_").Replace(":", "_") + ".xlsx";
                                }
                                
                                SalaryBillToExcel.WriteSalaryBillToExcel(salaryBill, fullFilePath);
                                db.EmployeeSalaryBills.Add(salaryBill);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(SalaryManagementSystem.Properties.Resources.SelectEmployeeMsg);
            }
        }

        private void EmployeesListView_Click(object sender, RoutedEventArgs e)
        {
            listViewColumnSorter.HandleColumnClick(sender, e);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetUserControl(new MainMenuUserControl());
        }
    }
}
