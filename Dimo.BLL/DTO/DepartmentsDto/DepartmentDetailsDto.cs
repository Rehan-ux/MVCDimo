using Dimo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.DTO.DepartmentsDto
{
    public class DepartmentDetailsDto

    {
        //public DepartmentDetailsDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    Description = department.Description;
        //    Code = department.Code;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value);
        //}
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
