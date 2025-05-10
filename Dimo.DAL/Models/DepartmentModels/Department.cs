using Dimo.DAL.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Models.DepartmentModels
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        //navigation prop [many]
        public virtual ICollection<Employee> Employees { get; set; }= new HashSet<Employee>();
    }
}
