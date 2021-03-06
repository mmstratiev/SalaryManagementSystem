﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace SalaryManagementSystem
{
    static class SalaryBillToExcel
    {
        static Application Excel;


        public static void WriteSalaryBillToExcel(EmployeeSalaryBill salaryBill, String path)
        {
            try
            {
                Application Excel = new Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                Workbook WorkBook = Excel.Workbooks.Add(Type.Missing);
                Worksheet WorkSheet = (Worksheet)WorkBook.ActiveSheet;
                WorkSheet.Name = salaryBill.Employee.Name;

                WorkSheet.Range[WorkSheet.Cells[1, 1], WorkSheet.Cells[1, 8]].Merge();
                WorkSheet.Cells[1, 1] = "Фиш за работна заплата";
                WorkSheet.Cells[1, 1].Font.Size = 15;


                WorkSheet.Range[WorkSheet.Cells[3, 1], WorkSheet.Cells[5, 2]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                WorkSheet.Cells[3, 1] = "Фирма: ";
                WorkSheet.Cells[3, 1] = salaryBill.Employee.CompanyName;

                WorkSheet.Cells[4, 1] = "ЕГН: ";
                WorkSheet.Cells[4, 2] = salaryBill.Employee.EGN;

                WorkSheet.Cells[5, 1] = "Име: ";
                WorkSheet.Cells[5, 2] = salaryBill.Employee.Name;

                WorkSheet.Cells[3, 7] = "Дата: ";
                WorkSheet.Cells[3, 8] = salaryBill.Date;


                WorkSheet.Range[WorkSheet.Cells[7, 1], WorkSheet.Cells[8, 2]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                WorkSheet.Cells[7, 1] = "Брутно тр. възнаграждение: ";
                WorkSheet.Cells[7, 2] = salaryBill.Employee.Salary;

                WorkSheet.Cells[8, 1] = "Данъчна основа: ";
                WorkSheet.Cells[8, 2] = salaryBill.DanychnaOsnova;
                
                WorkSheet.Range[WorkSheet.Cells[10, 1], WorkSheet.Cells[16, 3]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                WorkSheet.Range[WorkSheet.Cells[10, 1], WorkSheet.Cells[10, 3]].Borders.LineStyle = XlLineStyle.xlContinuous;
                WorkSheet.Cells[10, 1] = "Удръжки";
                WorkSheet.Cells[10, 2] = "Мярка %";
                WorkSheet.Cells[10, 3] = "Сума";

                WorkSheet.Cells[11, 1] = "ДДФЛ";
                WorkSheet.Cells[11, 2] = Constants.DDFL_PERCENTAGE;
                WorkSheet.Cells[11, 3] = salaryBill.DDFL;

                WorkSheet.Cells[12, 1] = "ДЗПО в УПФ";
                WorkSheet.Cells[12, 2] = Constants.DZPO_PERCENTAGE;
                WorkSheet.Cells[12, 3] = salaryBill.DZPO;

                WorkSheet.Cells[13, 1] = "ЗО";
                WorkSheet.Cells[13, 2] = Constants.ZO_PERCENTAGE;
                WorkSheet.Cells[13, 3] = salaryBill.ZO;

                WorkSheet.Cells[14, 1] = "ДОО Пенсии";
                WorkSheet.Cells[14, 2] = Constants.DOO_PENSII_PERCENTAGE;
                WorkSheet.Cells[14, 3] = salaryBill.DOO_PENSII;

                WorkSheet.Cells[15, 1] = "ДОО ОЗМ";
                WorkSheet.Cells[15, 2] = Constants.DOO_ZOM_PERCENTAGE;
                WorkSheet.Cells[15, 3] = salaryBill.DOO_ZOM;

                WorkSheet.Cells[16, 1] = "ДОО Безработица";
                WorkSheet.Cells[16, 2] = Constants.DOO_BEZRABOTICA_PERCENTAGE;
                WorkSheet.Cells[16, 3] = salaryBill.DOO_BEZRABOTICA;

                WorkSheet.Range[WorkSheet.Cells[17, 1], WorkSheet.Cells[18, 3]].BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);
                WorkSheet.Range[WorkSheet.Cells[17, 1], WorkSheet.Cells[17, 2]].Merge();
                WorkSheet.Cells[17, 1] = "Всико удръжки: ";
                WorkSheet.Cells[17, 3] = (salaryBill.Employee.Salary - salaryBill.NetSalary).ToString();

                WorkSheet.Range[WorkSheet.Cells[18, 1], WorkSheet.Cells[18, 2]].Merge();
                WorkSheet.Cells[18, 1] = "Сума за получаване: ";
                WorkSheet.Cells[18, 3] = salaryBill.NetSalary.ToString();

                WorkSheet.Columns.AutoFit();
                WorkSheet.Rows.AutoFit();
                WorkBook.SaveAs(path);

                Excel.Quit();
            } 
            catch (Exception ex)   
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                System.Windows.MessageBox.Show(errorMessage, "Error");
            }  
            finally   
            {
                Excel = null;
            }
        }
    }
}
