using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmployeesController(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }


        [HttpGet("{Search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await employeeRepo.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepo.GetEmployees());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var empEmail = await employeeRepo.GetEmployeeByEmail(employee.Email);
                //var empFirstName = employeeRepo.GetEmployeeByUserName(employee.FirstName);
                //if(empFirstName != null)
                //{
                //    ModelState.AddModelError("FirstName", "First name is used");
                //}
                if (empEmail != null)
                {
                    ModelState.AddModelError("Email", "Email Already in use");
                    return BadRequest(ModelState);
                }

                var result = await employeeRepo.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = result.EmployeeId }, result);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var deleteemployee = await employeeRepo.GetEmployee(id);
                if (deleteemployee == null)
                {
                    return NotFound($"can not find this id= {id}");
                }
                return await employeeRepo.DeleteEmployee(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");

            }
        }


      [HttpPut()]
      public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
               
                var empupdate = await employeeRepo.GetEmployee(employee.EmployeeId);
                if(empupdate == null)
                {
                    return NotFound($"this employee id = {employee.EmployeeId} not found");
                }
                return await employeeRepo.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating data");
            }
        } 

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepo.GetEmployee(id);
                if(result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");


            }
        }
    }
}
