using Dimo.BLL.DTO.EmployeesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Services.Interfaces
{
	public interface IEmployeeService
	{
		//Get All Emolyee
		IEnumerable<EmployeeDto> GetAllEmployees(bool withTracken = false);
        IEnumerable<EmployeeDto> SearchEmployeeByName(string name);

        EmployeeDetailsDto? GetEmployeeById(int id);	
		int CreateEmployee(CreatedEmployeeDto employee);
		int UpdateEmployee(UpdatedEmployeeDto employee);
		bool DeleteEmployee(int id);
	}
}
