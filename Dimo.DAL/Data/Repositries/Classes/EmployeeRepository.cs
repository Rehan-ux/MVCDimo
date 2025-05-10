using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models.DepartmentModels;
using Dimo.DAL.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Classes
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            return dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
        }
    }
}
