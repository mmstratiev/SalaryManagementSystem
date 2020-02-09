using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaryManagementSystem.Constants;

namespace SalaryManagementSystem.Model
{
    class EmployeeSalaryBill
    {
        public Employee Employee { get; }
        public String CompanyName { get; }
        public DateTime Date { get; }
        public double DDFL { get; }
        public double ZO { get; }
        public double DOO_PENSII { get; }
        public double DOO_ZOM { get; }
        public double DOO_BEZRABOTICA { get; }
        public double NetSalary { get; }

        public EmployeeSalaryBill(Employee employee, String companyName, DateTime date)
        {
            this.Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            this.CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
            this.Date = date;

            double SalaryForTaxes = employee.Salary > 3000 ? 3000 : employee.Salary;

            //Calculate taxes
            this.DDFL = employee.Salary * (Constants.Constants.DDFL_PERCENTAGE / 100);
            this.ZO = SalaryForTaxes * (Constants.Constants.ZO_PERCENTAGE / 100);
            this.DOO_PENSII = SalaryForTaxes * (Constants.Constants.DOO_PENSII_PERCENTAGE / 100);
            this.DOO_ZOM = SalaryForTaxes * (Constants.Constants.DOO_ZOM_PERCENTAGE / 100);
            this.DOO_BEZRABOTICA = SalaryForTaxes * (Constants.Constants.DOO_BEZRABOTICA_PERCENTAGE / 100);

            //NET SALARY
            this.NetSalary = employee.Salary - (this.DDFL + this.ZO + this.DOO_PENSII + this.DOO_ZOM + this.DOO_BEZRABOTICA);
        }
    }
}
