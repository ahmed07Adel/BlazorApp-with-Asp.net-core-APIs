using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        public Employee Employees { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = null;



        [Parameter]
        public string Id { get; set; }
        protected string Coordinate { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
         Employees =  await EmployeeService.GetEmployee(int.Parse(Id));
        }
        protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinate = $"X = {e.ClientX} Y = {e.ClientY}";
        }
        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide Footer";
            }
        }
    }
}
