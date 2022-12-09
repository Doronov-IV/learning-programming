global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Text;
global using System;

namespace ShootingRange
{
    public static class EmployeeService
    {
        public static async Task Main(string[] args)
        {
            EmployeeBusinessLogic businessLogic = new EmployeeBusinessLogic();
            businessLogic.SetDependency(new DefaultEmployeeDataAccess());
            Employee employeeDetails = businessLogic.GetEmployeeDetails(1);
            Console.WriteLine();
            Console.WriteLine("Employee Details:");
            Console.WriteLine($"ID : {employeeDetails.ID}, Name : {employeeDetails.Name}, Department : {employeeDetails.Department}, Salary : {employeeDetails.Salary}");
        }
    }
}