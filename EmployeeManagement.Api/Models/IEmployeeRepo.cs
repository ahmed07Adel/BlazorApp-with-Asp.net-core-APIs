using EmployeeManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
  public interface IEmployeeRepo
    {
        Task<IEnumerable> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> GetEmployeeByUserName(string Firstname);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeId);
        Task <IEnumerable<Employee>> Search(string name, Gender? gender);
    }
}
