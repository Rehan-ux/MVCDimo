using Dimo.BLL.DTO.DepartmentsDto;
using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models.DepartmentModels;

namespace Dimo.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}