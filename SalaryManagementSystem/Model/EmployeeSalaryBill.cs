using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SalaryManagementSystem
{
    class EmployeeSalaryBill
    { 
        [Key]
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public virtual Employee Employee {
            get
            {
                return _employee;
            }
            set
            {
                this._employee = value;
                CalculateProperties();
            }
        }
        [NotMapped]
        public double DDFL { get; private set; }
        [NotMapped]
        public double DZPO { get; private set; }
        [NotMapped]
        public double ZO { get; private set; }
        [NotMapped]
        public double DOO_PENSII { get; private set; }
        [NotMapped]
        public double DOO_ZOM { get; private set; }
        [NotMapped]
        public double DOO_BEZRABOTICA { get; private set; }
        [NotMapped]
        public double NetSalary { get; private set; }
        [NotMapped]
        public double DanychnaOsnova { get; private set; }
        [NotMapped]
        private Employee _employee;

        private EmployeeSalaryBill() { }

        public EmployeeSalaryBill(DateTime date, Employee employee)
        {
            this.Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            this.Date = date;
            CalculateProperties();
        }

        public void CalculateProperties()
        {
            double SalaryForTaxes = Employee.Salary > Constants.MAX_INSURANCE_INCOME ? Constants.MAX_INSURANCE_INCOME : Employee.Salary;

            //Calculate taxes

            this.DZPO = SalaryForTaxes * (Constants.DZPO_PERCENTAGE / 100);
            this.ZO = SalaryForTaxes * (Constants.ZO_PERCENTAGE / 100);
            this.DOO_PENSII = SalaryForTaxes * (Constants.DOO_PENSII_PERCENTAGE / 100);
            this.DOO_ZOM = SalaryForTaxes * (Constants.DOO_ZOM_PERCENTAGE / 100);
            this.DOO_BEZRABOTICA = SalaryForTaxes * (Constants.DOO_BEZRABOTICA_PERCENTAGE / 100);

            this.DanychnaOsnova = Employee.Salary - (this.DZPO + this.ZO + this.DOO_PENSII + this.DOO_ZOM + this.DOO_BEZRABOTICA);
            this.DDFL = DanychnaOsnova * (Constants.DDFL_PERCENTAGE / 100);

            //NET SALARY
            this.NetSalary = DanychnaOsnova - this.DDFL;
        }

        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
                result.AppendFormat("[{0}={1}] ", propertyInfo.Name, propertyInfo.GetValue(this));

            return result.ToString();
        }
    }
}
