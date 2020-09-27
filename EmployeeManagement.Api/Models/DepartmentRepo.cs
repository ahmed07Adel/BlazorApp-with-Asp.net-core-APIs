using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext context;

        public DepartmentRepo(AppDbContext context)
        {
            this.context = context;
        }

      


        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await context.Departments.ToListAsync();
        }



        public async Task<Department> GetDepartment(int departmentId)
        {
            return await context.Departments.FirstOrDefaultAsync(e => e.DepartmentId == departmentId);
        }
    }
}
