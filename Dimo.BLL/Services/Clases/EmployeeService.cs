﻿using AutoMapper;
using Dimo.BLL.DTO.EmployeesDto;
using Dimo.BLL.Services.AttatchMentServices;
using Dimo.BLL.Services.Interfaces;
using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Services.Clases
{
	public class EmployeeService(IUnitOfWork unitOfWork, IMapper _mapper,IAttachmentService attachmentService) : IEmployeeService
	{
		public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracken)
		{
			var Employees = unitOfWork.EmployeeRepository.GetAll(withTracken);
			//stc sorce = IEnumerable <Employee> دول اللي جاين من الدانا بيس 
			//Dest تحويل =IEnumerable <Employee Dto >
			var returnedEmployees = _mapper.Map < IEnumerable <Employee>,IEnumerable<EmployeeDto>>(Employees); //AutoMapper
			//var returnedEmployees = Employees.Select(emp => new EmployeeDto()
			//{
			//	Id= emp.Id,
			//	Name=emp.Name,
			//	Age=emp.Age,
			//	Email= emp.Email,
			//	Salary=emp.Salary,
			//	IsActive=emp.IsActive,
			//	EmployeeType=emp.EmployeeType.ToString(),
			//	Gender=emp.Gender.ToString()
			//});
			return returnedEmployees;
		}
        public IEnumerable<EmployeeDto> SearchEmployeeByName(string name)
        {
			var Employees = unitOfWork.EmployeeRepository.GetEmployeesByName(name.ToLower());
            //stc sorce = IEnumerable <Employee> دول اللي جاين من الدانا بيس 
            //Dest تحويل =IEnumerable <Employee Dto >
            var returnedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees); //AutoMapper
                                                                                                     //});
            return returnedEmployees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
		{
			var Employee = unitOfWork.EmployeeRepository.GetById(id);
			//if (Employee == null) return null;
			//else
			//{
			//	var returnedEmp = new EmployeeDetailsDto()
			//	{
			//		Id= Employee.Id,
			//		Name= Employee.Name,
			//		Age= Employee.Age,
			//		Email= Employee.Email,
			//		Salary= Employee.Salary,
			//		IsActive= Employee.IsActive,
			//		EmployeeType= Employee.EmployeeType.ToString(),
			//		Gender= Employee.Gender.ToString(),
			//		PhoneNumber= Employee.PhoneNumber,
			//		HiringDate=DateOnly.FromDateTime(Employee.HiringDate),
			//		CreatedBy= 1,
			//		LastModifiedBy=1,

			//	};
			//	return returnedEmp;
			//}
			return Employee is null ?null: _mapper.Map<Employee,EmployeeDetailsDto>(Employee);
		}
		public int CreateEmployee(CreatedEmployeeDto employeeDto)
		{
			var Employee = _mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);
			if(employeeDto.Image is not null)
			{
				Employee.ImageName = attachmentService.Upload(employeeDto.Image, "Images");
			}
		
			 unitOfWork.EmployeeRepository.Add(Employee);
			return unitOfWork.SaveChanges();
		}

		public bool DeleteEmployee(int id)//soft delete
		{
			var employee = unitOfWork.EmployeeRepository.GetById(id);

			if (employee == null) return false;
			else
			{
				employee.IsDeleted = true;
				employee.ImageName = null;
				unitOfWork.EmployeeRepository.Update(employee) ;
				int result = unitOfWork.SaveChanges() ;
				if (result > 0)
				{
					attachmentService.Delete(employee.ImageName, "Images");
					return true;
				}
				else
					return false;
				//return unitOfWork.SaveChanges() > 0 ? true : false;

            }
		}

		

		public int UpdateEmployee(UpdatedEmployeeDto employee)
		{
			 unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employee));
			return unitOfWork.SaveChanges();
		}
	}
}
