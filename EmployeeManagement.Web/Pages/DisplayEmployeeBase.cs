using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        [Parameter]
        public Employee Employees { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public EventCallback OnEmployeeDeleted { get; set; }

        protected EmployeeM.Components.ConfirmBase DeleteConfirmation { get; set; }
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employees.EmployeeId);
                await OnEmployeeDeleted.InvokeAsync(Employees.EmployeeId);
            }
        }
        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
    
        }
    }
}
