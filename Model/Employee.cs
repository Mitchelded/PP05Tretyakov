//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Linq;

namespace PP05Tretyakov.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        private decimal _amountEmployeesContract;
        public int Id_Employee { get; set; }
        public string FIO { get; set; }
        public string Department_Name { get; set; }
        public double Labor_Participation_Rate { get; set; }

        public decimal Amount_Employees_Contract
        {
            get
            {
                return _amountEmployeesContract;
            }
            set
            {
                if (Contract != null)
                {
                    // Calculate total sum of all employees' contracts in the contract
                    var totalSumm = Contract.Amount_Contract * (Contract.Wage_Fund / 100);
                    decimal summEmployee = 0;
                    
                    // Sum up amounts of contracts for all employees in the contract
                    foreach (var employee in Contract.Employee)
                    {
                        summEmployee += employee.Amount_Employees_Contract;
                    }

                    // Adjust the amount if the total exceeds the allowed sum
                    if (summEmployee > totalSumm)
                    {
                        _amountEmployeesContract = 0;
                    }
                    else
                    {
                        _amountEmployeesContract = value;
                    }
                }
                else
                {
                    _amountEmployeesContract = value;
                }
            }
        }
        public string Contract_Number { get; set; }
    
        public virtual Contract Contract { get; set; }
        public virtual Department Department { get; set; }
    }
}
