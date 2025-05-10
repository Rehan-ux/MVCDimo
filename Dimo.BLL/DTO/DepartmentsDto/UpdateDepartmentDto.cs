using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.DTO.DepartmentsDto
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }  //Entity fram work من خلاله هيقولي في update or not
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
        public string? Description { get; set; }
    }
}
