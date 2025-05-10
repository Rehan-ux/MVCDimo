using Dimo.BLL.DTO.DepartmentsDto;
using Dimo.DAL.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Factories
{
    static public class DepartmentFactory
    {
        //علشان كل ال method اللي جواه هتكون Extention method   هحول من department to departmentdto
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                Id = D.Id,
                Name = D.Name,
                Description = D.Description,
                Code = D.Code,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn.Value)
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                CreatedBy= department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto?.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }
        //method over loading
        public static Department ToEntity(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto?.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

    }
}
