using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagementSystem
{
    class Employee
    {
        [Key]
        public int      ID      { get; set; }
        [Required]
        public string   Name    { get; set; }
        [Required]
        public string   EGN     { get; set; }
        [Required]
        public double   Salary  { get; set; }
    }
}
