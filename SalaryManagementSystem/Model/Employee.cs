using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagementSystem
{
    class Employee
    {
        public const int MIN_NAME_LENGTH = 3;

        [Key]
        public int      ID          { get; set; }
        [Required]
        public string   Name        { get; set; }
        [Required]
        public string   EGN         { get; set; }
        [Required]
        public double   Salary      { get; set; }
        [Required]
        public string   CompanyName { get; set; }

        public static bool IsValidEGN(string EGN)
        {
            bool result = false;
            
            // EGN must be 10 digits
            if (EGN.Length == 10)
            {
                // EGN must contain only digits
                if (EGN.All(char.IsDigit))
                {
                    int day = int.Parse(EGN.Substring(4, 2));
                    int month = int.Parse(EGN.Substring(2, 2));
                    int year = 0;
                    if (month < 13)
                    {
                        year = int.Parse("19" + EGN.Substring(0, 2));
                    }
                    else if (month < 33)
                    {
                        month -= 20;
                        year = int.Parse("18" + EGN.Substring(0, 2));
                    }
                    else
                    {
                        month -= 40;
                        year = int.Parse("20" + EGN.Substring(0, 2));
                    }

                    // Check if date of birth is valid
                    DateTime dateOfBirth = new DateTime();
                    if (DateTime.TryParse(String.Format("{0}/{1}/{2}", day, month, year), out dateOfBirth))
                    {
                        int[] weights = new int[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
                        int totalControlSum = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            totalControlSum += weights[i] * (EGN[i] - '0');
                        }

                        int controlDigit = 0;
                        int reminder = totalControlSum % 11;

                        if (reminder < 10)
                        {
                            controlDigit = reminder;
                        }

                        // If last digit is the same as control digit, EGN is valid
                        result = (int.Parse(EGN.Substring(9)) == controlDigit);
                    }
                }
            }

            return result;
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
