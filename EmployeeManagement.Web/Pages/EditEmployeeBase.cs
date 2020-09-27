using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public string PageHeaderText { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        [Parameter]
        public string Id { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        [Inject]
        public IMapper Mapper { get; set; }


        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
       
        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int EmployeeId);
            if(EmployeeId != 0)
            {
                PageHeaderText = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));

            }
            else
            {
                PageHeaderText = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath = "images/icon-valet@2x.png"
                };
            }
          
            Departments = (await DepartmentService.GetDepartments()).ToList();
            Mapper.Map(Employee, EditEmployeeModel);
          

            //editEmployeeModel.EmployeeId = Employees.EmployeeId;
            //editEmployeeModel.FirstName = Employees.FirstName;
            //editEmployeeModel.LastName = Employees.LastName;
            //editEmployeeModel.Email = Employees.Email;
            //editEmployeeModel.ConfirmEmail = Employees.Email;
            //editEmployeeModel.DateOfBrith = Employees.DateOfBirth;
            //editEmployeeModel.Gender = Employees.Gender;
            //editEmployeeModel.PhotoPath = Employees.PhotoPath;
            //editEmployeeModel.DepartmentId = Employees.DepartmentId;
            //editEmployeeModel.Department = Employees.Department;


        }
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            Employee result = null;
            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }
            
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        protected async Task DeleteClick()
        {
           await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
