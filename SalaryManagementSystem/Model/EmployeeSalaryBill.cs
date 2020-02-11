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
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public virtual Employee Employee { get; set; }
        [NotMapped]
        public double DDFL { get; }
        [NotMapped]
        public double DZPO { get; }
        [NotMapped]
        public double ZO { get; }
        [NotMapped]
        public double DOO_PENSII { get; }
        [NotMapped]
        public double DOO_ZOM { get; }
        [NotMapped]
        public double DOO_BEZRABOTICA { get; }
        [NotMapped]
        public double NetSalary { get; }

        public EmployeeSalaryBill(DateTime Date, Employee Employee)
        {
            this.Employee = Employee ?? throw new ArgumentNullException(nameof(Employee));
            this.Date = Date;

            double SalaryForTaxes = Employee.Salary > Constants.MAX_INSURANCE_INCOME ? Constants.MAX_INSURANCE_INCOME : Employee.Salary;

            //Calculate taxes
            
            this.DZPO = SalaryForTaxes * (Constants.DZPO_PERCENTAGE / 100);
            this.ZO = SalaryForTaxes * (Constants.ZO_PERCENTAGE / 100);
            this.DOO_PENSII = SalaryForTaxes * (Constants.DOO_PENSII_PERCENTAGE / 100);
            this.DOO_ZOM = SalaryForTaxes * (Constants.DOO_ZOM_PERCENTAGE / 100);
            this.DOO_BEZRABOTICA = SalaryForTaxes * (Constants.DOO_BEZRABOTICA_PERCENTAGE / 100);
            
            double beforeDDFL = Employee.Salary - (this.ZO + this.DOO_PENSII + this.DOO_ZOM + this.DOO_BEZRABOTICA);
            this.DDFL = Employee.Salary - (beforeDDFL * Constants.DDFL_PERCENTAGE / 100);

            //NET SALARY
            this.NetSalary = Employee.Salary - (this.DDFL + beforeDDFL);
        }

        public EmployeeSalaryBill(){ }

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
