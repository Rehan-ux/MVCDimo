using Dimo.DAL.Models.DepartmentModels;
using Dimo.DAL.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Interfacies
{
	public interface IEmployeeRepository:IGenericRepository<Employee>
	{
		IQueryable<Employee> GetEmployeesByName (string name);



		//IEnumerable<Employee> GetAll(bool withTracking = false);
		////Get By Id  هترجع حاجه من نوعdepartment 
		//Employee GetById(int id);
		////Update هترجع ليا int  وبتاخد مني department تعمله update
		//int Update(Employee Entity);
		////Delete
		//int Delete(Employee Entity);//لو اكبر 
		//							  //insert
		//int Add(Employee Entity);
	}
}
