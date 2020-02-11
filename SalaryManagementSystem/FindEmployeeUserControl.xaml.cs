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

namespace SalaryManagementSystem
{
    /// <summary>
    /// Interaction logic for FindEmployeeUserControl.xaml
    /// </summary>
    public partial class FindEmployeeUserControl : UserControl
    {
        public FindEmployeeUserControl()
        {
            InitializeComponent();
        }

        List<Employee> searchResult;

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
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
    }
}
