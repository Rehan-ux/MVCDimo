using Dimo.BLL.DTO.DepartmentsDto;
using Dimo.BLL.Factories;
using Dimo.BLL.Services.Interfaces;
using Dimo.DAL.Data.Repositries.Classes;
using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Services.Clases
{
    //primary Constroctor
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        //هنا لوعملت constroctor هيعترض عليا 
        // private readonly IDepartmentRepository _departmentRepositry=departmentRepositry;  //عملت كومنت هنا لاانه مفيهوش حاجه زياده 

        //public DepartmentService(IDepartmentRepository _departmentRepositry)
        //{
        //    _departmentRepositry = departmentRepositry;
        //}
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()

        {
            //هشوف الحاجه االلي عايز ارجعها واعمل DDo
            var departments = _unitOfWork.DepartmentRepository.GetAll();


            //هعمل111 mapping  manual mappig
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    Id = D.Id,
            //    Name = D.Name,
            //    Description = D.Description,
            //    Code=D.Code,
            //    DateOfCreation=DateOnly .FromDateTime(D.CreatedOn.Value)
            //});
            //return departmentsToReturn;

            //22 Extention method
            return departments.Select(D => D.ToDepartmentDto()); //هتاخد pranter بتاعها من ال  caller

        }

        //Get Department by Id 
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //if (department is null) return null;
            //else
            //{
            //    var departmentToReturn = new DepartmentDetailsDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Description = department.Description,
            //        Code = department.Code,
            //        CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value)
            //    };
            //    return departmentToReturn;
            //}


            //Manual Mapping
            //Auto Mapper عباره عن  pakge بنزلها 
            //Constructor Mapping   بعمل mapping بس في Constrouctor
            //Extension method //لو مش كبير هنشتغل بيها 

            //11 manual mapping
            // return department is null ? null : new DepartmentDetailsDto(department)
            //{
            //Id = department.Id,
            //Name = department.Name,
            //Description = department.Description,
            //Code = department.Code,
            //CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value)
            // };
            //2 Extentionmethod
            return department is null ? null : department.ToDepartmentDetailsDto();

        }

        //Add Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();

        }

        //Update department
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        //Delete department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                 _unitOfWork.DepartmentRepository.Delete(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

    }
}
