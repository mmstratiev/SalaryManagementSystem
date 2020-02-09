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
    /// <summary>
    /// Interaction logic for AddEmployeeUserControl.xaml
    /// </summary>
    public partial class AddEmployeeUserControl : UserControl
    {
        public AddEmployeeUserControl()
        {
            InitializeComponent();
        }

        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z]+$");
        }

        private void SalaryTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double temp;
            e.Handled = !(double.TryParse(e.Text, out temp));
        }

        private void EGNTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int temp;
            e.Handled = !(int.TryParse(e.Text, out temp));
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

            double salary = 0;
            if (NameTextBox.Text.Length < Employee.MIN_NAME_LENGTH)
            {
                string nameMsg = SalaryManagementSystem.Properties.Resources.InvalidNameMsg;
                nameMsg = nameMsg.Replace("^1", Employee.MIN_NAME_LENGTH.ToString());

                MessageBox.Show( nameMsg
                                , "Error"
                                , MessageBoxButton.OK
                                , MessageBoxImage.Error);
            }
            else if (!Employee.IsValidEGN(EGNTextBox.Text))
            {
                MessageBox.Show ( SalaryManagementSystem.Properties.Resources.InvalidEGNMsg
                                , "Error"
                                , MessageBoxButton.OK
                                , MessageBoxImage.Error);
            }
            else if (   !double.TryParse(SalaryTextBox.Text, out salary) 
                    ||  salary <= Constants.Constants.MIN_SALARY)
            {
                string salaryMsg = SalaryManagementSystem.Properties.Resources.InvalidSalaryMsg;
                salaryMsg = salaryMsg.Replace("^1", Constants.Constants.MIN_SALARY.ToString());

                MessageBox.Show ( salaryMsg
                                , "Error"
                                , MessageBoxButton.OK
                                , MessageBoxImage.Error);
            }
            else
            {
                using (var db = new EmployeesDBContext())
                {
                    var query = from em in db.Employees
                                where em.EGN == EGNTextBox.Text
                                select em;

                    // EGN must be unique!
                    if (query.Count() == 0)
                    {
                        Employee newEmployee = new Employee()
                        {
                            Name = NameTextBox.Text,
                            EGN = EGNTextBox.Text,
                            Salary = salary
                        };

                        db.Employees.Add(newEmployee);
                        db.SaveChanges();

                        string addedMsg = SalaryManagementSystem.Properties.Resources.AddedEmployee;
                        addedMsg = addedMsg.Replace("^1", newEmployee.Name);
                        addedMsg = addedMsg.Replace("^2", newEmployee.EGN);

                        MessageBox.Show ( addedMsg
                                        , "Information"
                                        , MessageBoxButton.OK
                                        , MessageBoxImage.Information);
                    }
                    else
                    {
                        string alreadyExistsMsg = SalaryManagementSystem.Properties.Resources.EmployeeAlreadyExists;
                        alreadyExistsMsg = alreadyExistsMsg.Replace("^1", EGNTextBox.Text);

                        MessageBox.Show (alreadyExistsMsg
                                        , "Error"
                                        , MessageBoxButton.OK
                                        , MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
